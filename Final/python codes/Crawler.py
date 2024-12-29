import pyodbc
from datetime import datetime
import requests
from bs4 import BeautifulSoup

def webcrawler(P_url, number):
    server = 'DESKTOP-HLI3EMK'  # نام سرور
    driver = '{SQL Server}'
    database = 'AspStore'  # نام دیتابیس
    connection_string = f'DRIVER={driver};SERVER={server};DATABASE={database};Trust_Connection=yes'
    conn = pyodbc.connect(connection_string)
    cursor = conn.cursor()

    try:
        url = f"https://www.tgju.org/profile/{P_url}"
        # دریافت محتوای HTML از URL
        response = requests.get(url)
        response.raise_for_status()  # بررسی خطاهای احتمالی
        soup = BeautifulSoup(response.text, 'html.parser')

        # استخراج قیمت از HTML
        price_tag = soup.find('span', {'data-col': 'info.last_trade.PDrCotVal'})
        if not price_tag:
            return None

        new_price = price_tag.text.strip()

        # پیدا کردن رکورد از جدول products با id=number
        cursor.execute("SELECT Price, DateTime FROM Products WHERE Id = ?", (number,))
        product = cursor.fetchone()
        if not product:
            return None

        current_price = product[0]

        # مقایسه قیمت‌ها
        if current_price != new_price:
            now = datetime.now()
            formatted_date = now.strftime("%Y-%m-%d %H:%M:%S")

            # به‌روزرسانی قیمت و زمان در جدول products
            cursor.execute(
                "UPDATE Products SET Price = ?, DateTime = ? WHERE Id = ?",
                (new_price, formatted_date, number)
            )
            print(f"{P_url} updated to {new_price}")

            # اضافه کردن رکورد به جدول enrolls
            cursor.execute(
                "INSERT INTO Enrolls (Price, Time, IdP) VALUES (?, ?, ?)",
                (new_price, formatted_date, number)
            )
            print(f"{P_url}: new record inserted to Enrolls.")
        else:
            print(f"{P_url}: {current_price} no change.")

        # ذخیره تغییرات
        conn.commit()

    finally:
        # بستن اتصال به پایگاه داده
        cursor.close()
        conn.close()
