package com.cats.java;

import java.io.IOException;
import java.io.InputStream;
import java.net.URL;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.nio.file.StandardCopyOption;


public class DownloadImg extends Url {

    public String WriteImage(){
        String url = fetchUrl();
        String dest = "";
        System.out.println("Image taken from : " + url);
        try(InputStream in = new URL(url).openStream()){
            dest = System.getProperty("user.dir")+"\\cat_picture.jpg";
            Path to = Paths.get(dest); //convert from String to Path
            Files.copy(in, to, StandardCopyOption.REPLACE_EXISTING);
            System.out.println("Image saved locally at : " + dest);
        }catch(IOException e){
            System.out.println(e);
        }
        return dest;
    }
}
