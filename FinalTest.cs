using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;
using System.Net;
using Newtonsoft.Json.Linq;



class Result
{

    /*
     * Complete the 'getAuthorHistory' function below.
     *
     * The function is expected to return a STRING_ARRAY.
     * The function accepts STRING author as parameter.
     *
     * Base urls:
     *   https://jsonmock.hackerrank.com/api/article_users?username=
     *   https://jsonmock.hackerrank.com/api/articles?author=
     *
     */

    //  public class Author{
    //      String Username;
    //      String About;
    //      String Submitted;
    //      String Updated_at;
    //      int Submission_count;
    //      int Comment_count;
    //      int Created_at;
    //  }

    //  public class AuthorResponse{

    //      AuthorResponse(int page, List<Author> data){
    //          this.Page = page;
    //         //  this.Data = data;
    //      }
    //      int Page;
    //     //  List<Author> Data;

    //  }



    public static List<string> getAuthorHistory(string author)
    {
        string articleUsersUrl = "https://jsonmock.hackerrank.com/api/article_users?username=";
        // AuthorResponse output;
        string about;
        int totalPages;
        List<string> history = new List<string>();

        //https://jsonmock.hackerrank.com/api/article_users?username=<authorName>
        using (WebClient wc = new WebClient())
        {
            var json = wc.DownloadString(articleUsersUrl + author);
            JToken data = JObject.Parse(json).SelectToken("data");
            string aboutFromResponse = data[0].SelectToken("about").ToString();
            if (aboutFromResponse.Length > 0)
            {
                about = aboutFromResponse;
                history.Add(about);
            }

        }

        string articles = "https://jsonmock.hackerrank.com/api/articles?author=";

        // String totalPagesToken = JObject.Parse(json).SelectToken("total_pages").ToString();
        // totalPages = Int32.Parse(totalPagesToken);

        using (WebClient wc = new WebClient())
        {
            var json = wc.DownloadString(articles + author);
            String data = JObject.Parse(json).SelectToken("total_pages").ToString();
            totalPages = Int32.Parse(data);
        }

        for (int i = 1; i <= totalPages; i++)
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString(articles + author+"&page="+i.ToString());
                JToken data = JObject.Parse(json).SelectToken("data");
                foreach (var item in data)
                {

                    string title = item.SelectToken("title").ToString();
                    string story_title = item.SelectToken("story_title").ToString();

                    if (title.Length > 0)
                    {
                        history.Add(title);

                    }

                    if (title.Length == 0)
                    {
                        if (story_title.Length > 0)
                        {
                            history.Add(story_title);

                        }

                    }

                }

            }

        }

        return history;


        // List<string> returnValue = new List<String>();
        // return returnValue;

    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string author = Console.ReadLine();

        List<string> result = Result.getAuthorHistory(author);

        textWriter.WriteLine(String.Join("\n", result));

        textWriter.Flush();
        textWriter.Close();
    }
}
