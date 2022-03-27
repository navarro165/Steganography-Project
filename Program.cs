using System;

namespace P1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            // convert user input into a binary int array
            string[] user_input = args[0].Split(" ");
            string[] str_arr_input_bits = new string[user_input.Length];
            for (int i = 0; i < user_input.Length; i++) {
                byte b = Convert.ToByte(user_input[i], 16);
                str_arr_input_bits[i] = Convert.ToString(b, 2).PadLeft(8, '0');
            }

            char[] c_arr_input_bits = string.Join("", str_arr_input_bits).ToCharArray();
            int[] int_arr_input_bits = Array.ConvertAll(c_arr_input_bits, c => (int)Char.GetNumericValue(c));
            Console.WriteLine($"input_bits: {string.Join("", int_arr_input_bits)}");

            // 4x4 bitmap image
            byte[] bytes = {
                0x42 , 0x4D , 0x4C , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 ,
                0x00 , 0x00 , 0x1A , 0x00 , 0x00 , 0x00 , 0x0C , 0x00 ,
                0x00 , 0x00 , 0x04 , 0x00 , 0x04 , 0x00 , 0x01 , 0x00 ,
                0x18 , 0x00 , 0x00 , 0x00 , 0xFF , 0xFF , 0xFF , 0xFF ,
                0x00 , 0x00 , 0xFF , 0xFF , 0xFF , 0xFF , 0xFF , 0xFF ,
                0xFF , 0x00 , 0x00 , 0x00 , 0xFF , 0xFF , 0xFF , 0x00 ,
                0x00 , 0x00 , 0xFF , 0x00 , 0x00 , 0xFF , 0xFF , 0xFF ,
                0xFF , 0x00 , 0x00 , 0xFF , 0xFF , 0xFF , 0xFF , 0xFF ,
                0xFF , 0x00 , 0x00 , 0x00 , 0xFF , 0xFF , 0xFF , 0x00 ,
                0x00 , 0x00
            };

            Console.WriteLine($"\nheader: {BitConverter.ToString(bytes[..26]).Replace("-", " ")}");
            Console.WriteLine($"pixels: {BitConverter.ToString(bytes[26..]).Replace("-", " ")}\n");

            // inject two bits from input stream on the two lsb of each bytes per iteration
            int pixel_tracker = 26;
            for (int i = 0; i < int_arr_input_bits.Length; i+=2) {
                int j = i + 2;
                int byte_1, byte_2;
                byte_1 = int_arr_input_bits[i..j][0];
                byte_2 = int_arr_input_bits[i..j][1];

                char[] pixel_chr_array = Convert.ToString(bytes[pixel_tracker], 2).PadLeft(8, '0').ToCharArray();
                int[] pixel_int_array = Array.ConvertAll(pixel_chr_array, c => (int)Char.GetNumericValue(c));
                pixel_int_array[6] = pixel_int_array[6] ^ byte_1;
                pixel_int_array[7] = pixel_int_array[7] ^ byte_2;

                string final_string = string.Join(string.Empty, pixel_int_array);
                bytes[pixel_tracker] = Convert.ToByte(final_string, 2);
                pixel_tracker++;
            }
            // bitmap with hidden message
            Console.WriteLine($"{BitConverter.ToString(bytes[..]).Replace("-", " ")}");
        }
    }
}
