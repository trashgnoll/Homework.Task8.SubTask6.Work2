namespace ConsoleApp4
{
    internal class Program
    {
        public static string InputString(string prompt, bool allowEmptyInput = false)
        {
            Console.Write(prompt);
            string? result;
            if (allowEmptyInput)
                result = Console.ReadLine();
            else
                while ((result = Console.ReadLine()) is null || result == string.Empty)
                    Console.Write(prompt);
            return (result is not null ? result : string.Empty);
        }
        public static long TotalSize = 0;
        public static void GetSize(string path)
        {
            DirectoryInfo directoryInfo = new(path);
            // Add files size:
            IEnumerable<FileInfo> files = directoryInfo.GetFiles();
            foreach (FileInfo fi in files)
                TotalSize += fi.Length;
            // Check inner folders:
            IEnumerable<DirectoryInfo> folders = directoryInfo.GetDirectories();
            foreach (DirectoryInfo di in folders)
            {
                try { GetSize(di.FullName); }
                catch (Exception e)
                {
                    Console.WriteLine("Error with " + di.FullName + ": " + e.Message);
                    continue;
                }
            }
        }
        static void Main(string[] args)
        {
            string inputPrompt = "Select folder to calculate size: ";
            string path = (args.Length == 0 ? InputString(inputPrompt) : args[0]);
            DirectoryInfo directoryInfo = new(path);
            while (!directoryInfo.Exists)
            {
                Console.WriteLine("Directory not exists.");
                path = InputString(inputPrompt);
                directoryInfo = new(path);
            }
            try { GetSize(path); }
            catch (Exception e)
            { 
                Console.WriteLine("Error with " + path + ": " + e.Message); 
            }
            Console.WriteLine("Total size: " + TotalSize.ToString());
        }
    }
}