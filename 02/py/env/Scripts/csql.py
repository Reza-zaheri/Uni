import pyodbc
import requests
from bs4 import BeautifulSoup

# لیست‌ها برای ذخیره اطلاعات
title = []
price = []
num_shop = []
pics=[]
# URL
url = 'https://torob.com/search/?category=&query=%DA%AF%D9%88%D8%B4%DB%8C%20%D8%B3%D8%A7%D9%85%D8%B3%D9%88%D9%86%DA%AF&page=1'

# ارسال درخواست
res = requests.get(url)
res.encoding = 'utf-8'
html_content = res.text
soup = BeautifulSoup(html_content, 'html.parser')

# استخراج داده‌ها
titl = soup.select('h2.ProductCard_desktop_product-name__JwqeK')
pi = soup.select('div.ProductCard_desktop_product-price-text__y20OV')
nums = soup.select('div.ProductCard_desktop_shops__mbtsF')
pic=soup.select('div.ProductImageSlider_slide__kN_Ed')
for div in pic:
    img = div.find('img')
    pics.append(img['src'])
for t in titl:
    title.append(t.text.strip())
for p in pi:
    price.append(p.text.strip())
for n in nums:
    num_shop.append(n.text.strip())
    print(pics)
# بررسی داده‌ها
if not (title and price and num_shop):
    print("No data found. Check your selectors or site restrictions.")
    exit()

# تبدیل داده‌ها به فرمت پایگاه داده
records = [(title[i], price[i], num_shop[i],pics[i]) for i in range(len(title))]

# اتصال به پایگاه داده
server = 'DESKTOP-HLI3EMK'  # نام سرور
driver = '{SQL Server}'
database = 'pycrawl'  # نام دیتابیس
connection_string = f'DRIVER={driver};SERVER={server};DATABASE={database};Trust_Connection=yes'

try:
    conn = pyodbc.connect(connection_string)
    cursor = conn.cursor()

    # دستور SQL برای وارد کردن داده‌ها
    sql_insert = '''
        INSERT INTO product (p_title, p_price, p_shop,p_img)
        VALUES (?, ?, ?,?)
    '''

    # اجرای دستور
    cursor.executemany(sql_insert, records)
    conn.commit()
    print("Data successfully inserted into the database.")

except Exception as e:
    conn.rollback()
    print(f"An error occurred: {str(e)}")

finally:
    # بستن اتصال
    cursor.close()
    conn.close()
