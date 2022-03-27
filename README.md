# Steganography-Project
## Hide a hidden message in a bitmap image

This program takes in a 12 hexadecimal set of digits as input (e.g., B1 FF FF CC 98 80 09 EA 04 48 7E C9) and hides it inside the following bitmap (16 pixel) image.

Since the image has 16 pixels, there are 3*16 = 48 color bytes. Hiding 2 bits per byte allows us to hide 48 * 2 = 96 bits / 8 bits/byte = 12 bytes of data.
We hide the values using the XOR operator on the 2 least significant bits on each byte of data (exluding the headers).

<img width="324" alt="Screen Shot 2022-03-27 at 12 21 15 PM" src="https://user-images.githubusercontent.com/43856967/160293057-a1572775-dbe5-4311-aa0c-3cc856c56211.png">


### Instructions
* Create a .NET project with the attached .cs file
* Run the following command (arg here is sample of data that can be injected) ```dotnet run "B1 FF FF CC 98 80 09 EA 04 48 7E C9"```
* The output will look like:
```
input_bits: 101100011111111111111111110011001001100010000000000010011110101000000100010010000111111011001001

header: 42 4D 4C 00 00 00 00 00 00 00 1A 00 00 00 0C 00 00 00 04 00 04 00 01 00 18 00
pixels: 00 00 FF FF FF FF 00 00 FF FF FF FF FF FF FF 00 00 00 FF FF FF 00 00 00 FF 00 00 FF FF FF FF 00 00 FF FF FF FF FF FF 00 00 00 FF FF FF 00 00 00

Bitmap with hidden message (resulting image looks just the same as the sample above):
42 4D 4C 00 00 00 00 00 00 00 1A 00 00 00 0C 00 00 00 04 00 04 00 01 00 18 00 02 03 FF FE FC FC 03 03 FC FC FC FC FC FF FC 00 02 01 FD FF FD 00 00 00 FF 00 02 FE FC FD FD 02 00 FF FE FF FE FF FD 00 01 03 FC FD FC 00 02 01```
