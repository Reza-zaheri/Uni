import pyodbc
from datetime import datetime
import requests
from bs4 import BeautifulSoup
from Crawler import webcrawler 

# تعریف لیست از رشته‌ها
string_list = ["price_dollar_rl", "geram18", "sana_sell_eur", "sana_sell_aed","geram24","sekee","sekeb","nim","rob","price_kwd","sana_sell_cny","gold_mini_size"]

id_value = None

# پیمایش بین اندیس‌های لیست
for value in string_list:

    # استفاده از match-case برای اعمال شرط روی مقادیر لیست
    match value:
        case "geram18":
            id_value = 2
        case "price_dollar_rl":
            id_value = 5
        case "sana_sell_aed":
            id_value = 6
        case "sana_sell_eur":
            id_value = 7
        case "geram24":
            id_value = 9
        case "sekee":
            id_value = 10
        case "sekeb":
            id_value = 11
        case "nim":
            id_value = 12
        case "rob":
            id_value = 13
        case "price_kwd":
            id_value = 14
        case "sana_sell_cny":
            id_value = 15
        case "gold_mini_size":
            id_value = 16
        case _:
            break

    # فراخوانی تابع با مقدار لیست و مقدار id
    webcrawler(value, id_value)

