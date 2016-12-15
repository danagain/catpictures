package com.cats.java;

import com.sun.jna.Library;
import com.sun.jna.Native;
import com.sun.jna.win32.W32APIOptions;

public class Main {

    public static interface User32 extends Library {
        User32 INSTANCE = (User32) Native.loadLibrary("user32",User32.class,W32APIOptions.DEFAULT_OPTIONS);
        boolean SystemParametersInfo (int one, int two, String s ,int three);
    }

    public static void main(String[] args) {
        System.out.println("Cat Pictures Java Edition ;)");
        DownloadImg img = new DownloadImg();
        String diskPath = img.WriteImage();
        User32.INSTANCE.SystemParametersInfo(0x0014, 0,
                diskPath , 1);

    }
}
