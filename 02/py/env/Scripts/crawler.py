import requests as rq
from bs4 import BeautifulSoup
url='https://www.skysports.com/womens-super-league-table'
response=rq.get(url)
soup=BeautifulSoup(response.text,'html.parser')
table=soup.find('table')
rows=table.find_all('tr')
for row in rows:
    data = []
    for thead in row.find_all('th'):
        head=thead.text.replace('\n','')
        data.append(head)
    for td in row.find_all('td'):
        tdata=td.text.replace('\n','')
        data.append(tdata)
    print(data)



