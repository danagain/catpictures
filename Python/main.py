import requests
from bs4 import BeautifulSoup
from random import randint
import shutil
import re
import ctypes
import os
SPI_SETDESKTOPWALLPAPER=20

def getAllUrls():

    try:
        request = requests.get('https://octodex.github.com/', stream=True)
        if request.status_code == 200:
           regex = r'(?<=data-src=")/images/.*(?=" height="424" width="424)'
           soup = BeautifulSoup(request.text, "html.parser")
           text = soup.find_all('a')
           urlArray = []

           for value in text:
               result = re.search(regex, str(value))
               if result != None:
                   urlArray.append(result.group(0))
           return urlArray
    except Exception as e:
        print(e)


def downloadImg(urlArray):

    index = randint(0,len(urlArray))
    try:
        completeUrl = 'https://octodex.github.com{0}'.format(urlArray[index])
        req = requests.get(completeUrl, stream=True)
        if req.status_code == 200:

            with open("output.png", 'wb') as f:
                req.raw.decode_content = True
                shutil.copyfileobj(req.raw, f)
                picturePath = '{0}/output.png'.format(os.getcwd())
                return picturePath
    except Exception as e:
        print(e)


def setWallpaper(Path):
    ctypes.windll.user32.SystemParametersInfoW(SPI_SETDESKTOPWALLPAPER, 0, Path, 3)

def main():
    Urls = getAllUrls()
    picturePath = downloadImg(Urls)
    setWallpaper(picturePath)


if __name__ == '__main__':
    main()
