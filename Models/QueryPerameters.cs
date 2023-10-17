namespace MyFirstAPI.Models
{
    public class QueryPerameters
    {
        public int Page { get; set; } = 1; // Default value of 1, meaning it will return the first page
        public int Size { 
            get { return _size; } // returns the value of _size
            set { _size = Math.Min(_maxSize, value);} // Sets the value of _size to the minimum of _maxSize and the value, so it will never be greater than _maxSize
               } 

        const int _maxSize = 100; // Max value of 100, meaning it will return 100 results
        private int _size = 50; // Default value of 100


    }
}
