import pyodbc
from datetime import datetime
import requests
from bs4 import BeautifulSoup
from Crawler import webcrawler 

# تعریف لیست از رشته‌ها
string_list = ["price_dollar_rl", "geram18", "sana_sell_eur", "sana_sell_aed","geram24"]

# تعریف id_value در بیرون از حلقه
id_value = None

# پیمایش بین اندیس‌های لیست
for value in string_list:

    # استفاده از match-case برای اعمال شرط روی مقادیر لیست
    match value:
        case "price_dollar_rl":
            id_value = 5
        case "geram18":
            id_value = 2
        case "sana_sell_eur":
            id_value = 7
        case "sana_sell_aed":
            id_value = 6
        case "geram24":
            id_value = 8
        case _:
            break

    # فراخوانی تابع با مقدار لیست و مقدار id
    webcrawler(value, id_value)

