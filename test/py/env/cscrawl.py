import requests
from bs4 import BeautifulSoup
import pyodbc

def crawler(base_url, page_number):
    title = []
    price = []
    pics = []

    # ساخت URL
    url = f"{base_url}?page={page_number}&avalablePro=1&f1=0&f2=0&f3=0&f4=0"

    # هدر برای درخواست


    # ارسال درخواست
    response = requests.get(url)
    if response.status_code != 200:
        print("خطا در بارگیری صفحه")
        return None

    # پردازش با BeautifulSoup
    soup = BeautifulSoup(response.text, 'html.parser')
    product_areas = soup.select('div.product-detail-area')
    img=soup.select('a.product-image')
    # اگر محصولی پیدا نشد
    if len(product_areas)==0:
        return None

    # استخراج اطلاعات
    for product in product_areas:
        # عنوان
        title_tag = product.find('a')
        if title_tag:
            title.append(title_tag.text.strip())
        # قیمت
        price_tag = product.select_one('span.kalastore-price.amount')
        if price_tag:
            price.append(price_tag.text.strip())
    #تصاویر
    for i in img:
        img_tag=i.find('img')
        pics.append(img_tag['src'])
    min_length = min(len(title), len(price), len(pics))
    Result = [(title[i], price[i], pics[i]) for i in range(min_length)]
    # خروجی
    return Result

# اتصال به پایگاه داده
server = 'DESKTOP-HLI3EMK'  # نام سرور
driver = '{SQL Server}'
database = 'pycrawl'  # نام دیتابیس
connection_string = f'DRIVER={driver};SERVER={server};DATABASE={database};Trust_Connection=yes'

# استفاده از تابع
base_url = "https://berozkala.com/products/159/%D9%85%D8%AD%D8%A7%D9%81%D8%B8-%D8%A8%D8%B1%D9%82-%D9%88-%DA%86%D9%86%D8%AF-%D8%B1%D8%A7%D9%87%DB%8C"
i=1
while True:
    products = crawler(base_url, i)
    if products is None:
        break
    if len(products) == 0:
        break
    i+=1
    try:
        conn = pyodbc.connect(connection_string)
        cursor = conn.cursor()

        # دستور SQL برای وارد کردن داده‌ها
        sql_insert = '''
            INSERT INTO pr_digis (p_title, p_price,p_img)
            VALUES (?, ?, ?)
        '''

        # اجرای دستور
        cursor.executemany(sql_insert, products)
        conn.commit()
        print("Data successfully inserted into the database.")

    except Exception as e:
        conn.rollback()
        print(f"An error occurred: {str(e)}")

    finally:
        # بستن اتصال
        cursor.close()
        conn.close()


