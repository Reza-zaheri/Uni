import requests
from bs4 import BeautifulSoup
import pandas as pd
import time

# لیست‌ها برای ذخیره اطلاعات
title = []
price = []
num_shop = []

# URL
url = 'https://torob.com/search/?category=&query=%DA%AF%D9%88%D8%B4%DB%8C%20%D8%B3%D8%A7%D9%85%D8%B3%D9%88%D9%86%DA%AF&page=1'

# هدر برای شبیه‌سازی مرورگر
headers = {
    'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36'
}

# ارسال درخواست
res = requests.get(url, headers=headers)
res.encoding = 'utf-8'  # تنظیم کدگذاری
html_content = res.text
soup = BeautifulSoup(html_content, 'html.parser')

# استخراج اطلاعات
titl = soup.select('h2.ProductCard_desktop_product-name__JwqeK')  # تطبیق انتخابگرها
pi = soup.select('div.ProductCard_desktop_product-price-text__y20OV')
nums = soup.select('div.ProductCard_desktop_shops__mbtsF')

# بررسی و افزودن به لیست‌ها
for t in titl:
    ti = t.text.strip()
    title.append(ti)
for p in pi:
    pr = p.text.strip()
    price.append(pr)
for n in nums:
    nn = n.text.strip()
    num_shop.append(nn)

# چاپ داده‌ها برای بررسی
print("Titles:", title)
print("Prices:", price)
print("Shop Numbers:", num_shop)

# ذخیره در DataFrame و اکسل
if title and price and num_shop:  # بررسی خالی نبودن لیست‌ها
    product = {'Title': title, 'Price': price, 'ShopNumb': num_shop}
    df = pd.DataFrame(product)
    with pd.ExcelWriter('product.xlsx', engine='openpyxl') as writer:
        df.to_excel(writer, index=False, sheet_name='Sheet1')
    print("Data saved to product.xlsx")
else:
    print("No data extracted. Check the selectors or website restrictions.")
