"""Downloads a Random Github Octocat Picture and Sets it as Desktop Wallpaper"""
import shutil
import re
import ctypes
import os
from random import randint
import requests
from bs4 import BeautifulSoup


SPI_SETDESKTOPWALLPAPER = 20

def getURL():
"""Gets a Random URL from the Octodex"""
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
            index = randint(0, len(urlArray))
            completeUrl = 'https://octodex.github.com{0}'.format(urlArray[index])
            return completeUrl
    except Exception as e:
        print(e)


def download_Img(completeUrl):
"""Downloads the Image from the passed in URL"""
    try:
        req = requests.get(completeUrl, stream=True)
        if req.status_code == 200:

            with open("output.png", 'wb') as f:
                req.raw.decode_content = True
                shutil.copyfileobj(req.raw, f)
                picturePath = '{0}/output.png'.format(os.getcwd())
                return picturePath
    except Exception as err:
        print(err)


def set_Wallpaper(Path):
    """Sets the Wallpaper (Windows) with the newly downloaded Image."""
    ctypes.windll.user32.SystemParametersInfoW(SPI_SETDESKTOPWALLPAPER, 0, Path, 0)

def main():
    Urls = getURL()
    picturePath = download_Img(Urls)
    set_Wallpaper(picturePath)


if __name__ == '__main__':
    main()
