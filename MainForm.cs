using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SQLite;

// Taber Henderson
// 19 November 2021
// SQLite Test app for researching SQL Injection flaws
// (Source of inspiration and humor: https://xkcd.com/327/)

namespace SQLiteBrowse
{
   public partial class MainForm : Form
   {
      const string AppName = "SQLite Browse Test";

      //const string DbName = "LocalData.db";
      const string DbName = ":memory:";

      SQLiteConnection connection;

      static string[] SearchFavoriteStrings = new string[]
      {
         "Robert' OR 'a'='a",
         "Robert'; SELECT Name FROM Students;--",
         "Robert'; DROP TABLE Students; --",
      };

      ContextMenuStrip contextMenuFavorites;
      ToolStripMenuItem menuItemMode_Unsafe;
      ToolStripMenuItem menuItemMode_Escaped;
      ToolStripMenuItem menuItemMode_Parameterized;

      enum QueryExecType
      {
         Unsafe,
         Escaped,
         Parameterized,
      }

      QueryExecType queryExecMode = QueryExecType.Parameterized;

      public MainForm()
      {
         InitializeComponent();         

         this.textBoxQueryInput.KeyDown += TextBoxQueryInput_KeyDown;

         this.buttonRunQuery.Click += ButtonRunQuery_Click;
         this.buttonClear.Click += ButtonClear_Click;

         // add context menu "favorite" name queries
         this.contextMenuFavorites = new ContextMenuStrip();
         for( int i = 0; i < SearchFavoriteStrings.Length; ++i )
         {
            this.contextMenuFavorites.Items.Add( SearchFavoriteStrings[i], null, OnContextMenuFavorites_Clicked );
         }

         // add context menu options to change the SQL query execution mode
         this.contextMenuFavorites.Items.Add( "-" );
         this.menuItemMode_Unsafe = this.contextMenuFavorites.Items.Add( nameof( QueryExecType.Unsafe ), null, OnContextMenuMode_Clicked ) as ToolStripMenuItem;
         this.menuItemMode_Escaped = this.contextMenuFavorites.Items.Add( nameof( QueryExecType.Escaped ), null, OnContextMenuMode_Clicked ) as ToolStripMenuItem;
         this.menuItemMode_Parameterized = this.contextMenuFavorites.Items.Add( nameof( QueryExecType.Parameterized ), null, OnContextMenuMode_Clicked ) as ToolStripMenuItem;

         this.textBoxQueryInput.ContextMenuStrip = this.contextMenuFavorites;

         UpdateExecutionMode();
      }

      void OnContextMenuFavorites_Clicked( object sender, EventArgs e )
      {
         if( sender is ToolStripMenuItem menuItem )
         {
            this.textBoxQueryInput.Text = menuItem.Text;
            this.textBoxQueryInput.SelectAll();
         }
      }

      void UpdateExecutionMode()
      {
         this.Text = $"{AppName} - ({this.queryExecMode})";

         this.menuItemMode_Unsafe.Checked = this.queryExecMode == QueryExecType.Unsafe;
         this.menuItemMode_Escaped.Checked = this.queryExecMode == QueryExecType.Escaped;
         this.menuItemMode_Parameterized.Checked = this.queryExecMode == QueryExecType.Parameterized;
      }

      void OnContextMenuMode_Clicked( object sender, EventArgs e )
      {
         if( sender is ToolStripMenuItem menuItem && Enum.TryParse( menuItem.Text, out QueryExecType newExecMode ) )
         {
            // change query execution method type
            this.queryExecMode = newExecMode;
            // refresh window title and context menu selection
            UpdateExecutionMode();
         }
      }

      protected override void OnLoad( EventArgs e )
      {
         DBInit();

         base.OnLoad( e );
      }

      private void DBInit()
      {
         this.connection = new SQLiteConnection( $"Data Source = {DbName};" );
         this.connection.Open();


         SQLiteCommand createCmd = connection.CreateCommand();
         createCmd.CommandText = "CREATE TABLE Students (Name TEXT, Age INT, Grade INT, Score INT)";
         createCmd.ExecuteNonQuery();

         string[] columnValues = new string[]
            {
               // Name, Age, Grade, Score
               "'Andrew', 10, 5, 90",
               "'Benjamin', 10, 5, 87",
               "'Crystal', 10, 5, 98",
               "'Dorthy', 10, 5, 94",
               "'Evelyn', 10, 5, 92",
               "'Felicity', 10, 5, 85",
               "'George', 10, 5, 80",
               "'Auli''i', 9, 5, 100",
            };

         SQLiteCommand insertCmd = connection.CreateCommand();
         for( int i = 0; i < columnValues.Length; ++i )
         {
            insertCmd.CommandText = $"INSERT INTO Students (Name , Age, Grade , Score) VALUES ({columnValues[i]})";
            insertCmd.ExecuteNonQuery();
         }


         // test read all columns from in-memory db
         SQLiteCommand readCmd = this.connection.CreateCommand();
         readCmd.CommandText = "SELECT * FROM Students";
         SQLiteDataReader resultReader = readCmd.ExecuteReader();

         System.Diagnostics.Debug.WriteLine( "\nTable Contents:\n" );

         // output results from query to debug output
         DisplayQueryResults( resultReader, s => System.Diagnostics.Debug.Write( s ) );

         System.Diagnostics.Debug.WriteLine( "\n" );
      }

      void ButtonClear_Click( object sender, EventArgs e )
      {
         ClearAllText();
      }

      void ClearAllText()
      {
         this.textBoxQueryInput.Clear();
         this.textBoxOutput.Clear();
      }

      void ButtonRunQuery_Click( object sender, EventArgs e )
      {
         RunQueryUserInput();
      }

      void TextBoxQueryInput_KeyDown( object sender, KeyEventArgs e )
      {
         switch( e.KeyCode )
         {
            case Keys.Enter:
            {
               RunQueryUserInput();
               e.SuppressKeyPress = true;
               break;
            }
            case Keys.Escape:
            {
               ClearAllText();
               e.SuppressKeyPress = true;
               break;
            }
         }
      }

      void RunQueryUserInput()
      {
         string matchName = this.textBoxQueryInput.Text.Trim();

         // clear current text output
         this.textBoxOutput.Clear();

         try
         {
            if( matchName != string.Empty )
            {
               switch( this.queryExecMode )
               {
                  case QueryExecType.Unsafe:
                  {
                     //MethodInfo methodInfo = GetType().GetMethod( "ExecuteNameMatchQuery_Unsafe", BindingFlags.Instance | BindingFlags.NonPublic );
                     //if( methodInfo != null )
                     //{
                     //   methodInfo.Invoke( this, new object[] { matchName } );
                     //}
                     //else
                     {
                        ExecuteNameMatchQuery_Unsafe( matchName );
                        //ExecuteNameMatchQuery_Escaped( matchName );
                     }
                     break;
                  }
                  case QueryExecType.Escaped:
                  {
                     ExecuteNameMatchQuery_Escaped( matchName );
                     break;
                  }
                  case QueryExecType.Parameterized:
                  {
                     ExecuteNameMatchQuery_Parameterized( matchName );
                     break;
                  }
               }
            }
         }
         catch( Exception e )
         {
            this.textBoxOutput.AppendText( e.ToString() );
         }
      }

      void ExecuteNameMatchQuery_Unsafe( string studentName )
      {
         string adjStudentName = "'" + studentName.ToLower() + "'";

         const string nameMatchQueryString = "SELECT * FROM Students WHERE LOWER( Name ) = ";

         SQLiteCommand readCmd = this.connection.CreateCommand();
         readCmd.CommandText = nameMatchQueryString + adjStudentName;

         SQLiteDataReader resultReader = readCmd.ExecuteReader();

         // clear current text output
         this.textBoxOutput.Clear();

         // output results from query to text box
         DisplayQueryResults( resultReader );
      }

      static string EscapeUserInputString( string userInput )
      {
         return "'" + userInput.Replace( "'", "''" ) + "'";
      }

      void ExecuteNameMatchQuery_Escaped( string studentName )
      {
         string escapedStudentName = EscapeUserInputString( studentName.ToLower() );

         const string nameMatchQueryString = "SELECT * FROM Students WHERE LOWER( Name ) = ";

         SQLiteCommand readCmd = this.connection.CreateCommand();
         readCmd.CommandText = nameMatchQueryString + escapedStudentName;

         SQLiteDataReader resultReader = readCmd.ExecuteReader();

         // output results from query to text box
         DisplayQueryResults( resultReader );
      }

      void ExecuteNameMatchQuery_Parameterized( string studentName )
      {
         studentName = studentName.ToLower();

         const string nameMatchQueryString = "SELECT * FROM Students WHERE LOWER( Name ) = @SearchName";

         SQLiteCommand readCmd = this.connection.CreateCommand();
         readCmd.CommandText = nameMatchQueryString;
         readCmd.Parameters.AddWithValue( "@SearchName", studentName );

         SQLiteDataReader resultReader = readCmd.ExecuteReader();

         // clear current text output
         this.textBoxOutput.Clear();

         // output results from query to text box
         DisplayQueryResults( resultReader );
      }

      void DisplayQueryResults( SQLiteDataReader resultReader, Action<string> writeText = null )
      {
         if( writeText == null )
         {
            writeText = ( string s ) => this.textBoxOutput.AppendText( s );
         }

         if( resultReader.HasRows )
         {
            int cols = resultReader.FieldCount;
            object[] colVals = new object[cols];

            // display column headers
            for( int i = 0; i < cols; ++i )
            {
               string colName = resultReader.GetName( i );
               writeText( colName + "\t" );
            }
            writeText( Environment.NewLine );

            while( resultReader.Read() )
            {
               writeText( Environment.NewLine );

               // retrieve array of column values
               resultReader.GetValues( colVals );

               // join all column values with tab separator
               writeText( string.Join( "\t", colVals ) );

               // add newline char
               writeText( Environment.NewLine );
            }
         }
         else
         {
            writeText( "No Results" );
         }
      }
   }
}
