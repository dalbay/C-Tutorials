# C# Working with Files

## System.IO Namespace

| class      | Description         
| ------------- |:-------------:|  
|   BinaryReader    | Reads primitive data from a binary stream. |
| BinaryWriter | Writes primitive data in binary format. |
| BufferedStream | A temporary storage for a stream of bytes. |
| Directory | Helps in manipulating a directory structure.  |
| DirectoryInfo | Used for performing operations on directories.|
| DriveInfo | Provides information for the drives. |
| File | Helps in manipulating files. | 
| FileInfo | Used for performing operations on files. |
| FileStream | Used to read from and write to any location in a file. |
| MemoryStream | Used for random access to streamed data stored in memory. |
| Path | Performs operations on path information.  |
| StreamReader | Used for reading characters from a byte stream.|
| StreamWriter | Is used for writing characters to a stream. |
| StringReader | Is used for reading from a string buffer. | 
| StringWriter | Is used for writing into a string buffer. |

![system io namespace](images/ioImg.png)

### File Class
- Namespace: System.IO
- Assemblies: System.IO.FileSystem.dll, mscorlib.dll, netstandard.dll
- *Provides static methods for the creation, copying, deletion, moving, and opening of a single file, and aids in the creation of FileStream objects.*
***Example:***  
```C#
using namespace System;
using namespace System::IO;
int main()
{
   String^ path = "c:\\temp\\MyTest.txt";
   if (  !File::Exists( path ) )
   {
      
      // Create a file to write to.
      StreamWriter^ sw = File::CreateText( path );
      try
      {
         sw->WriteLine( "Hello" );
         sw->WriteLine( "And" );
         sw->WriteLine( "Welcome" );
      }
      finally
      {
         if ( sw )
                  delete (IDisposable^)(sw);
      }
   }

   // Open the file to read from.
   StreamReader^ sr = File::OpenText( path );
   try
   {
      String^ s = "";
      while ( s = sr->ReadLine() )
      {
         Console::WriteLine( s );
      }
   }
   finally
   {
      if ( sr )
            delete (IDisposable^)(sr);
   }

   try
   {
      String^ path2 = String::Concat( path, "temp" );
      
      // Ensure that the target does not exist.
      File::Delete( path2 );
      
      // Copy the file.
      File::Copy( path, path2 );
      Console::WriteLine( "{0} was copied to {1}.", path, path2 );
      
      // Delete the newly created file.
      File::Delete( path2 );
      Console::WriteLine( "{0} was successfully deleted.", path2 );
   }
   catch ( Exception^ e ) 
   {
      Console::WriteLine( "The process failed: {0}", e );
   }
}
```
#### Methods:
- *AppendAllLines()*  
- *AppendAllText()* - Opens a file, appends the specified string to the file, and then closes the file. If the file does not exist, this method creates a file, writes the specified string to the file, then closes the file.
- *AppendText()* - Creates a StreamWriter that appends UTF-8 encoded text to an existing file, or to a new file if the specified file does not exist.
- *Copy()* - Copies an existing file to a new file. Overwriting a file of the same name is not allowed.
- *Create()* - Creates or overwrites a file in the specified path.
- *Decrypt()*
- *Delete()* - Deletes the specified file.
- *Encrypt()*
- *Exists()* - Determines whether the specified file exists.
- *GetAccessControl()*
- *GetAttributes()*
- *GetCreationTime()* - Returns the creation date and time of the specified file or directory.
- *GetLastAccessTime()* - Returns the date and time the specified file or directory was last accessed.  
- *GetLastWriteTime()* - Returns the date and time the specified file or directory was last written to.  
- *Move()* - Moves a specified file to a new location, providing the option to specify a new file name.
- *Open()*
- *OpenRead(String)*
- *OpenWrite(String)*
- *ReadAllBytes(String)*
- *ReadAllLines(String)*
- *ReadAllText(String)*
- *ReadLines(String)*
- . . .
<br/>

### Directory Class
- Namespace: System.IO
- Assemblies: System.IO.FileSystem.dll, mscorlib.dll, netstandard.dll
- *Exposes static methods for creating, moving, and enumerating through directories and subdirectories. This class cannot be inherited.*  
***Example:***  
The following example shows how to retrieve all the text files from a directory and move them to a new directory. After the files are moved, they no longer exist in the original directory.
```C#
using System;
using System.IO;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceDirectory = @"C:\current";
            string archiveDirectory = @"C:\archive";

            try
            {
                var txtFiles = Directory.EnumerateFiles(sourceDirectory, "*.txt");

                foreach (string currentFile in txtFiles)
                {
                    string fileName = currentFile.Substring(sourceDirectory.Length + 1);
                    Directory.Move(currentFile, Path.Combine(archiveDirectory, fileName));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
#### Methods:
- *CreateDirectory()* - Creates all directories and subdirectories in the specified path unless they already exist.
- *Delete()* - Deletes an empty directory from a specified path.
- *EnumerateDirectories()*
- *Exists()* - Determines whether the given path refers to an existing directory on disk.
- *GetCreationTime()* - Gets the creation date and time of a directory.
- *GetCurrentDirectory()* - Gets the current working directory of the application.
- *GetDirectories()* - Returns the names of subdirectories (including their paths) in the specified directory.
- *EnumerateFiles()* - Returns an enumerable collection of full file names in a specified path.
- *GetFiles()* - Returns the names of files (including their paths) in the specified directory.
- *GetParent()* - Retrieves the parent directory of the specified path, including both absolute and relative paths.
- *Move()* - Moves a file or a directory and its contents to a new location.
- *SetCurrentDirectory()* - Sets the application's current working directory to the specified directory.  
- . . .
<br/>

### FileInfo Class
- Namespace: System.IO
- Assemblies: System.IO.FileSystem.dll, mscorlib.dll, netstandard.dll
- *Provides properties and instance methods for the creation, copying, deletion, moving, and opening of files, and aids in the creation of FileStream objects. This class cannot be inherited.*
- Use the FileInfo class for typical operations such as copying, moving, renaming, creating, opening, deleting, and appending to files.
- If you are performing multiple operations on the same file, it can be more efficient to use FileInfo instance methods instead of the corresponding static methods of the File class, because a security check will not always be necessary.
- Many of the FileInfo methods return other I/O types when you create or open files. You can use these other types to further manipulate a file. 
- By default, full read/write access to new files is granted to all users.

#### Constructors
- *FileInfo(String)* - Initializes a new instance of the FileInfo class, which acts as a wrapper for a file path.
#### Fields
- *FullPath* - Represents the fully qualified path of the directory or file.(Inherited from FileSystemInfo)
- *OriginalPath* - The path originally specified by the user, whether relative or absolute.(Inherited from FileSystemInfo)
#### Properties
- *Attributes* - Gets or sets the attributes for the current file or directory.
- *CreationTime* - Gets or sets the creation time of the current file or directory.
- *Directory* - Gets an instance of the parent directory.
- *DirectoryName* - Gets a string representing the directory's full path.
- *Exists* - Gets a value indicating whether a file exists.
- *Extension* - Gets the string representing the extension part of the file.
- *FullName	* - Gets the full path of the directory or file.
- *IsReadOnly* - Gets or sets a value that determines if the current file is read only.
- *LastAccessTime* - Gets or sets the time the current file or directory was last accessed.
- *LastWriteTime* - Gets or sets the time when the current file or directory was last written to.
- *Length* - Gets the size, in bytes, of the current file.
- *Name* - Gets the name of the file.

#### Methods
- *AppendText()*- Creates a StreamWriter that appends text to the file represented by this instance of the FileInfo.
- *CopyTo(String)* - Copies an existing file to a new file, disallowing the overwriting of an existing file.
- *Create()* - Creates a file.
- *CreateText()* - Creates a StreamWriter that writes a new text file.
- *Delete()* - Permanently deletes a file.
- *Equals(Object)* - Determines whether the specified object is equal to the current object.
- *GetType()* - Gets the Type of the current instance.
- *MoveTo(String)* - Moves a specified file to a new location, providing the option to specify a new file name.
- *Open(FileMode)* - Opens a file in the specified mode.
- *Replace(String, String, Boolean)* - Replaces the contents of a specified file with the file described by the current FileInfo object, deleting the original file, and creating a backup of the replaced file. Also specifies whether to ignore merge errors.
- *ToString()* - Returns the path as a string. Use the Name property for the full path.
- . . .  
<br/>

***Example:***  

The following example demonstrates some of the main members of the FileInfo class.  
When the properties are first retrieved, FileInfo calls the Refresh method and caches information about the file. On subsequent calls, you must call Refresh to get the latest copy of the information.  

```C#
using namespace System;
using namespace System::IO;

int main()
{
   String^ path = Path::GetTempFileName();
   FileInfo^ fi1 = gcnew FileInfo( path );
   //Create a file to write to.
   StreamWriter^ sw = fi1->CreateText();
   try
   {
     sw->WriteLine( "Hello" );
     sw->WriteLine( "And" );
     sw->WriteLine( "Welcome" );
   }
   finally
   {
     if ( sw )
        delete (IDisposable^)sw;
   }

   //Open the file to read from.
   StreamReader^ sr = fi1->OpenText();
   try
   {
      String^ s = "";
      while ( s = sr->ReadLine() )
      {
         Console::WriteLine( s );
      }
   }
   finally
   {
      if ( sr )
         delete (IDisposable^)sr;
   }

   try
   {
      String^ path2 = Path::GetTempFileName();
      FileInfo^ fi2 = gcnew FileInfo( path2 );

      //Ensure that the target does not exist.
      fi2->Delete();

      //Copy the file.
      fi1->CopyTo( path2 );
      Console::WriteLine( "{0} was copied to {1}.", path, path2 );

      //Delete the newly created file.
      fi2->Delete();
      Console::WriteLine( "{0} was successfully deleted.", path2 );
   }
   catch ( Exception^ e )
   {
      Console::WriteLine( "The process failed: {0}", e );
   }
}
```

### DirectoryInfo Class
- Namespace: System.IO
- Assemblies: System.IO.FileSystem.dll, mscorlib.dll, netstandard.dll
- *Exposes instance methods for creating, moving, and enumerating through directories and subdirectories. This class cannot be inherited.*
- Use the DirectoryInfo class for typical operations such as copying, moving, renaming, creating, and deleting directories.
- If you are going to reuse an object several times, consider using the instance method of DirectoryInfo instead of the corresponding static methods of the Directory class, because a security check will not always be necessary.  

#### Constructors
- *DirectoryInfo(String)* - Initializes a new instance of the DirectoryInfo class on the specified path.
#### Fields
- *FullPath* - Represents the fully qualified path of the directory or file.(Inherited from FileSystemInfo)
- *OriginalPath* - The path originally specified by the user, whether relative or absolute.(Inherited from FileSystemInfo)
#### Properties
- *Attributes* - Gets or sets the attributes for the current file or directory.
- *CreationTime* - Gets or sets the creation time of the current file or directory.
- *Exists* - Gets a value indicating whether a file exists.
- *Extension* - Gets the string representing the extension part of the file.
- *FullName* - Gets the full path of the directory or file.
- *LastAccessTime* - Gets or sets the time the current file or directory was last accessed.
- *LastWriteTime* - Gets or sets the time when the current file or directory was last written to.
- *Name* - Gets the name of the file.
- *Parent* - Gets the parent directory of a specified subdirectory.
- *Root* - Gets the root portion of the directory.

#### Methods
- *Create()* - Creates a Directory.
- *Delete()* - Deletes this DirectoryInfo if it is empty.
- *EnumerateDirectories()* - Returns an enumerable collection of directory information in the current directory.
- *EnumerateFiles()* - Returns an enumerable collection of file information in the current directory.
- *Equals(Object)* - Determines whether the specified object is equal to the current object.
- *GetDirectories()* - Returns the subdirectories of the current directory.
- *GetFiles()* - Returns a file list from the current directory.
- *GetType()* - Gets the Type of the current instance.
- *MoveTo(String)* - Moves a DirectoryInfo instance and its contents to a new path.
- *Refresh()* - Refreshes the state of the object. (Inherited from FileSystemInfo)
- *ToString()* - Returns the original path that was passed to the DirectoryInfo constructor. Use the FullName or Name properties for the full path or file/directory name instead of this method.  
- . . .  
<br/>  

***Example:***  
The following example demonstrates some of the main members of the DirectoryInfo class.
```C#
using namespace System;
using namespace System::IO;
int main()
{
   
   // Specify the directories you want to manipulate.
   DirectoryInfo^ di = gcnew DirectoryInfo( "c:\\MyDir" );
   try
   {
      
      // Determine whether the directory exists.
      if ( di->Exists )
      {
         
         // Indicate that the directory already exists.
         Console::WriteLine( "That path exists already." );
         return 0;
      }
      
      // Try to create the directory.
      di->Create();
      Console::WriteLine( "The directory was created successfully." );
      
      // Delete the directory.
      di->Delete();
      Console::WriteLine( "The directory was deleted successfully." );
   }
   catch ( Exception^ e ) 
   {
      Console::WriteLine( "The process failed: {0}", e );
   }

}
```  
<br/>

### FileStream Class
- Namespace: System.IO
- Assemblies: System.IO.FileSystem.dll, mscorlib.dll, netstandard.dll, System.Runtime.dll
- *Provides a Stream for a file, supporting both synchronous and asynchronous read and write operations.*
- Use the ***FileStream*** class to read from, write to, open, and close files on a file system, and to manipulate other file-related operating system handles, including pipes, standard input, and standard output.
- You can use the ***Read***, ***Write***, ***CopyTo***, and ***Flush*** methods to perform ***synchronous operations***.
- You can use the ***ReadAsync***, ***WriteAsync***, ***CopyToAsync***, and ***FlushAsync*** methods to perform ***asynchronous operations***.
#### Constructors 
- *FileStream(IntPtr, FileAccess)* - Initializes a new instance of the FileStream class for the specified file handle, with the specified read/write permission...
- . . . 

#### Properties
- *CanRead* - Gets a value that indicates whether the current stream supports reading.
- *IsAsync* - Gets a value that indicates whether the FileStream was opened asynchronously or synchronously.
- *Length* - Gets the length in bytes of the stream.
- *Name* - Gets the absolute path of the file opened in the FileStream.
- . . .  

#### Methods
- *BeginRead(Byte[], Int32, Int32, AsyncCallback, Object)* - Begins an asynchronous read operation. Consider using ReadAsync(Byte[], Int32, Int32, CancellationToken) instead.
- *BeginWrite(Byte[], Int32, Int32, AsyncCallback, Object)* - Begins an asynchronous write operation. Consider using WriteAsync(Byte[], Int32, Int32, CancellationToken) instead.
- *CopyTo()* - Reads the bytes from the current stream and writes them to another stream.
- *CopyToAsync()* - Asynchronously reads the bytes from the current stream and writes them to another stream.
- *Dispose()* - Releases all resources used by the Stream.
- *Finalize()* - Ensures that resources are freed and other cleanup operations are performed when the garbage collector reclaims the FileStream.
- *Flush()* - Clears buffers for this stream and causes any buffered data to be written to the file.
- *Read(Byte[], Int32, Int32)* - Reads a block of bytes from the stream and writes the data in a given buffer.
- *WriteAsync(Byte[], Int32, Int32)* - Asynchronously writes a sequence of bytes to the current stream and advances the current position within this 
- . . .  
<br/>

***Example:***  
The following example shows how to write to a file asynchronously. This code runs in a WPF app that has a TextBlock named UserInput and a button hooked up to a Click event handler that is named Button_Click. The file path needs to be changed to a file that exists on the computer.  

```C#
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace WpfApplication1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            UnicodeEncoding uniencoding = new UnicodeEncoding();
            string filename = @"c:\Users\exampleuser\Documents\userinputlog.txt";
           
            byte[] result = uniencoding.GetBytes(UserInput.Text);
            
            using (FileStream SourceStream = File.Open(filename, FileMode.OpenOrCreate))
            {
                SourceStream.Seek(0, SeekOrigin.End);
                await SourceStream.WriteAsync(result, 0, result.Length);
            }
        }
    }
}  
```  
<br/>

***Example:***  
The following example demonstrates how to use two *FileStream* objects to asynchronously copy the files from one directory to another directory. The **FileStream** class derives from the **Stream** class. Notice that the Click event handler for the Button control is marked with the ```async``` modifier because it calls an asynchronous method.  
```C#
using System;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace WpfApplication
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string StartDirectory = @"c:\Users\exampleuser\start";
            string EndDirectory = @"c:\Users\exampleuser\end";

            foreach (string filename in Directory.EnumerateFiles(StartDirectory))
            {
                using (FileStream SourceStream = File.Open(filename, FileMode.Open))
                {
                    using (FileStream DestinationStream = File.Create(EndDirectory + filename.Substring(filename.LastIndexOf('\\'))))
                    {
                        await SourceStream.CopyToAsync(DestinationStream);
                    }
                }
            }
        }
    }
}
```  
### StreamReader Class
- Namespace: System.IO
- Assemblies: System.IO.dll, mscorlib.dll, netstandard.dll, System.Runtime.Extensions.dll
- *Implements a TextReader that reads characters from a byte stream in a particular encoding.*
- Use StreamReader for reading lines of information from a standard text file.
- StreamReader defaults to UTF-8 encoding unless specified otherwise
- By default, a StreamReader is not thread safe.

#### Constructors
- *StreamReader(Stream)* - Initializes a new instance of the StreamReader class for the specified stream.
- . . .

#### Fields
- *Null* - A StreamReader object around an empty stream.

#### Properties
- *BaseStream* - Returns the underlying stream.
- *CurrentEncoding* - Gets the current character encoding that the current StreamReader object is using.
- *EndOfStream* - Gets a value that indicates whether the current stream position is at the end of the stream.

#### Methods
- *Close()* - Closes the StreamReader object and the underlying stream, and releases any system resources associated with the reader.
- *DiscardBufferedData()* - Clears the internal buffer.
- *Dispose()* - Releases all resources used by the TextReader object.
- *Peek()* - Returns the next available character but does not consume it.
- *Read()* -Reads the next character from the input stream and advances the character position by one character.
- *ReadAsync(Char[], Int32, Int32)* -Reads a specified maximum number of characters from the current stream asynchronously and writes the data to a buffer, beginning at the specified index.
- *ReadBlock(Char[], Int32, Int32)* - Reads a specified maximum number of characters from the current stream and writes the data to a buffer, beginning at the specified index.
- *ReadLine()* - Reads a line of characters from the current stream and returns the data as a string.
- *ReadLineAsync()* - Reads a line of characters asynchronously from the current stream and returns the data as a string.
- *ReadToEnd()* - Reads all characters from the current position to the end of the stream.
- . . .  
<br/>

***Example:***  
The following example instantiates a StreamReader object and calls its ReadAsync method to read a file asynchronously.  
```C#
using System;
using System.IO;
using System.Threading.Tasks;

class Example
{
    static async Task Main()
    {
        await ReadAndDisplayFilesAsync();
    }

    static async Task ReadAndDisplayFilesAsync()
    {
        String filename = "TestFile1.txt";
        Char[] buffer;
        
        using (var sr = new StreamReader(filename)) {
            buffer = new Char[(int)sr.BaseStream.Length];
            await sr.ReadAsync(buffer, 0, (int)sr.BaseStream.Length);
        }

        Console.WriteLine(new String(buffer));
    }
}
// The example displays the following output:
//       This is the first line of text in a relatively short file.
//       This is the second line.
//       This is the third line.
//       This is the fourth and final line.
```  
### StreamWriter Class
- Namespace: System.IO
- Assemblies: System.IO.dll, mscorlib.dll, netstandard.dll, System.Runtime.Extensions.dll
- *Implements a TextWriter for writing characters to a stream in a particular encoding.*
- StreamWriter is designed for character output in a particular encoding, whereas classes derived from Stream are designed for byte input and output.

---  
