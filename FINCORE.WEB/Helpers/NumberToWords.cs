namespace FINCORE.WEB.Helpers
{
    public class NumberToWords
    {
        private static readonly string[] numberInWord = { "", "Satu", "Dua", "Tiga", "Empat", "Lima", "Enam", "Tujuh",
                "Delapan", "Sembilan", "Sepuluh", "Sebelas" };

        public static string ConvertNumberToWords(long number)
        {
            long utama = 0;
            int depan = 0;
            long belakang = 0;

            if (number < 12) // 1 - 11
                return numberInWord[number];

            if (number < 20) // 12 - 19
                return $"{numberInWord[number - 10]} Belas";

            if (number < 100) // 20 - 99
            {
                SplitNumber(number, 10, out utama, out depan, out belakang);
                return numberInWord[depan] + " Puluh " + numberInWord[belakang];
            }

            if (number < 200) // 100 - 199
                return "Seratus " + ConvertNumberToWords(number - 100);

            if (number < 1000) // 200 - 999
            {
                SplitNumber(number, 100, out utama, out depan, out belakang);
                return numberInWord[depan] + " Ratus " + ConvertNumberToWords(belakang);
            }

            if (number < 2000) // 1,000 - 1,999
                return "Seribu " + ConvertNumberToWords(number - 1000);

            if (number < 10_000) // 2,000 - 9,999
            {
                SplitNumber(number, 100, out utama, out depan, out belakang);
                return numberInWord[depan] + " Ribu " + ConvertNumberToWords(belakang);
            }

            if (number < 100_000) // 10,000 - 99,999
            {
                SplitNumber(number, 1000, out utama, out depan, out belakang, 2, 1000);
                return ConvertNumberToWords(depan) + " Ribu " + ConvertNumberToWords(belakang);
            }

            if (number < 1_000_000) // 100,000 - 999,999
            {
                SplitNumber(number, 1000, out utama, out depan, out belakang, 3);
                return ConvertNumberToWords(depan) + " Ribu " + ConvertNumberToWords(belakang);
            }

            if (number < 10_000_000) // 1,000,000 - 	99,999,999
            {
                SplitNumber(number, 1_000_000, out utama, out depan, out belakang);
                return ConvertNumberToWords(depan) + " Juta " + ConvertNumberToWords(belakang);
            }

            if (number < 100_000_000) // 1,000,000 - 	99,999,999
            {
                SplitNumber(number, 1_000_000, out utama, out depan, out belakang, 2);
                return ConvertNumberToWords(depan) + " Juta " + ConvertNumberToWords(belakang);
            }

            if (number < 1_000_000_000)
            {
                SplitNumber(number, 1_000_000, out utama, out depan, out belakang, 3);
                return ConvertNumberToWords(depan) + " Juta " + ConvertNumberToWords(belakang);
            }

            if (number < 10_000_000_000) // new
            {
                SplitNumber(number, 1_000_000_000, out utama, out depan, out belakang);
                return ConvertNumberToWords(depan) + " Milyar " + ConvertNumberToWords(belakang);
            }

            if (number < 100_000_000_000)
            {
                SplitNumber(number, 1000000000, out utama, out depan, out belakang, 2);
                return ConvertNumberToWords(depan) + " Milyar " + ConvertNumberToWords(belakang);
            }

            if (number < 1_000_000_000_000)
            {
                SplitNumber(number, 1000000000, out utama, out depan, out belakang, 3);
                return ConvertNumberToWords(depan) + " Milyar " + ConvertNumberToWords(belakang);
            }

            if (number < 10_000_000_000_000)
            {
                SplitNumber(number, 10000000000, out utama, out depan, out belakang);
                return ConvertNumberToWords(depan) + " Triliun " + ConvertNumberToWords(belakang);
            }

            if (number < 100_000_000_000_000)
            {
                SplitNumber(number, 1000000000000, out utama, out depan, out belakang, 2);
                return ConvertNumberToWords(depan) + " Triliun " + ConvertNumberToWords(belakang);
            }

            if (number < 1_000_000_000_000_000)
            {
                SplitNumber(number, 1000000000000, out utama, out depan, out belakang, 3);
                return ConvertNumberToWords(depan) + " Triliun " + ConvertNumberToWords(belakang);
            }

            if (number < 10_000_000_000_000_000)
            {
                SplitNumber(number, 10_000_000_000_000_000, out utama, out depan, out belakang);
                return ConvertNumberToWords(depan) + " Kuadriliun " + ConvertNumberToWords(belakang);
            }

            return "Angka tidak dikenali";
        }

        private static void SplitNumber(long number, long devider,
            out long utama, out int depan, out long belakang,
            int mainDigitLength = 1,
            long? secondDevider = null)
        {
            utama = number / devider;

            depan = Convert.ToInt32(utama.ToString()[..mainDigitLength]);

            if (secondDevider is not null)
            {
                belakang = (long)(number % secondDevider);
            }
            else
            {
                belakang = number % devider;
            }
        }
    }
}