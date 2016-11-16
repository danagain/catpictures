package main

import (
  "fmt"
 "net/http"
 "log"
 "io/ioutil"
 "io"
 "regexp"
 "os"
 "time"
 "math/rand"
 "bytes"
 "path/filepath"
 "syscall"
 "unsafe"
)

var (
    user32               = syscall.NewLazyDLL("user32.dll") //user32.dll
    systemParametersInfo = user32.NewProc("SystemParametersInfoW")
)

func directory(){
  path,_ := filepath.Abs("Background")
  if _, err := os.Stat(path); os.IsNotExist(err) {
      os.Mkdir(path, os.ModePerm)
  }
}

func getHtml()string{
  fmt.Println("Downloading HTML code for the desired webpage")
  resp, err := http.Get("https://octodex.github.com/")
  if err != nil {
  log.Fatal(err)
}
defer resp.Body.Close()//Client must close the response body when finished with it
body, err := ioutil.ReadAll(resp.Body)
if err != nil {
log.Fatal(err)
}
return string(body)//Typecasting bytes to string format and reutrn for regexp
}

func imageStrings(source string)string{
  r, err := regexp.Compile("images/(.*)jpg")
  if err != nil {
      log.Fatal(err)
  }
  titles := r.FindAllString(source, -1)
  for i:=0; i < len(titles); i++{
    fmt.Printf("\n%s", titles[i])
  }
 rand.Seed(time.Now().UTC().UnixNano())//seed timer to system clock
  rngNum := rand.Int63n(int64(len(titles)))
  var buffer bytes.Buffer  //max efficiency concating strings use byte buffer
  buffer.WriteString("https://octodex.github.com/")
  buffer.WriteString(titles[rngNum])
  return buffer.String()
}

func downloadImage(url string)string{
  fmt.Printf("\n\nURL = %s", url)
  path,_ := filepath.Abs("Background")
  path2 :=  path+string(os.PathSeparator) // adding / to end of path
  finalPath := path2+"image.jpg"
  img, _ := os.Create(finalPath)
defer img.Close()
resp, _ := http.Get(url)
defer resp.Body.Close()
b, _ := io.Copy(img, resp.Body)
fmt.Println("\nFile size in bytes: ", b)
fmt.Println("\nFile is now Saved in " + finalPath)
return finalPath
}

func main(){
src := getHtml()
link := imageStrings(src)
directory()
imageLocation := downloadImage(link)
fmt.Println("Setting BG to: " + imageLocation)
img, _, _ := systemParametersInfo.Call(20, 0, uintptr(unsafe.Pointer(syscall.StringToUTF16Ptr(imageLocation))), 2)
fmt.Printf("%d", img)
}
