package com.cats.java;

import org.jsoup.Jsoup;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import java.util.concurrent.ThreadLocalRandom;


public class Url {

    public String fetchUrl(){
        String htmlCode = "";
        List<String> urls = new ArrayList<>();
        try{
            htmlCode = Jsoup.connect("https://octodex.github.com/").get().html();
            String htmlString = Jsoup.parse(htmlCode).toString();
            String pattern =  "/images/(.*).jpg";
            // Create a Pattern object
            Pattern r = Pattern.compile(pattern);
            //Using scanner to scan source code line by line
            Scanner scanner = new Scanner(htmlString);
            while (scanner.hasNextLine()) {
                String line = scanner.nextLine();
                // process the line
                Matcher m = r.matcher(line);
                if (m.find( )) {
                    System.out.println("Found URL: " + m.group(0));
                    urls.add(m.group(0).toString());
                }
            }
            scanner.close();
        } catch (IOException e){
            System.out.println("Exception thrown  :" + e);
        }
        // nextInt is normally exclusive of the top value,
        // so add 1 to make it inclusive
        int rng = ThreadLocalRandom.current().nextInt(0, urls.size() + 1);
        return "https://octodex.github.com/"+urls.get(rng);
    }
}
