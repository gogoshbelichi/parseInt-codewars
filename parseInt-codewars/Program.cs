//hash table to associate words to numbers and digits
var ht = new Dictionary<string, int>()
{
    { "zero", 0 },
    { "one", 1 },
    { "two", 2 },
    { "three", 3 },
    { "four", 4 },
    { "five", 5 },
    { "six", 6 },
    { "seven", 7 },
    { "eight", 8 },
    { "nine", 9 },
    { "ten", 10 },
    { "eleven", 11 },
    { "twelve", 12 },
    { "thirteen", 13 },
    { "fourteen", 14 },
    { "fifteen", 15 },
    { "sixteen", 16 },
    { "seventeen", 17 },
    { "eighteen", 18 },
    { "nineteen", 19 },
    { "twenty", 20 },
    { "thirty", 30 },
    { "forty", 40 },
    { "fifty", 50 },
    { "sixty", 60 },
    { "seventy", 70 },
    { "eighty", 80 },
    { "ninety", 90 },
    { "hundred", 100 },
    { "thousand", 1000 },
    { "one million", 1000000 },
    { "and", 0 }
    //is it ok that two different keys are pointed to same value(zero & and)
    //it's ok but i need to refresh knowledge about collision
};
//split chars
char[] delimiterChars = [' ', '-'];
//temp result
int parsedInt;
//main func
void ParseInt(string s)
{
    if (s == "one million")
    {
        Console.WriteLine(1000000);
        return;
    }
    //we make our number backwards that's why we LINQ Reverse() the splited input string  
    var splited = s.Split(delimiterChars).Reverse().ToArray();
    //initializing list for indices of word "hundred" to make parsing easier
    //this is where FindAllIndices() func will store results
    List<int> indices = FindAllIndices(splited, "hundred");
    parsedInt = 0;
    //cycle to traverse reversed splited input string
    for (int i = 0; i < splited.Length; i++)
    {
        //merged cases
        if (splited.Contains("hundred") && splited.Contains("thousand"))
        {
            //thousand-hundred case: "six thousand nine hundred and one" //6901
            if (indices.Count == 1 && Array.IndexOf(splited, "hundred") < Array.IndexOf(splited, "thousand"))
            {
                if (i < indices[0])
                {
                    parsedInt += ht[splited[i]];
                }
                if (i > indices[0] && i < Array.IndexOf(splited, "thousand"))
                {
                    parsedInt += (ht[splited[i]])*100;
                }
                if (i > Array.IndexOf(splited, "thousand"))
                {
                    parsedInt += (ht[splited[i]])*1000;
                }
            }
            //hundred-thousand case: "7 hundred 68 thousand 55" //768055
            if (indices.Count == 1 && Array.IndexOf(splited, "hundred") > Array.IndexOf(splited, "thousand"))
            {
                if (i < Array.IndexOf(splited, "thousand"))
                {
                    parsedInt += ht[splited[i]];
                }
                if (i < indices[0] && i > Array.IndexOf(splited, "thousand"))
                {
                    parsedInt += (ht[splited[i]])*1000;
                }
                if (i > indices[0])
                {
                    parsedInt += (ht[splited[i]])*1000*100;
                }
            }
            //hundred-thousand-hundred case: //985732
            if (indices.Count == 2)
            {
                if (i < indices[0])
                {
                    parsedInt += ht[splited[i]];
                }
                if (i > indices[0] && i < Array.IndexOf(splited, "thousand"))
                {
                    parsedInt += (ht[splited[i]])*100;
                }
                if (i < indices[1] && i > Array.IndexOf(splited, "thousand"))
                {
                    parsedInt += (ht[splited[i]])*1000;
                }
                if (i > indices[1])
                {
                    parsedInt += (ht[splited[i]])*1000*100;
                }
            }
            
        }
        //thousand only case
        if (splited.Contains("thousand") && !splited.Contains("hundred")) //example: "twenty-eight thousand seventy-five"
        {
            if (i < Array.IndexOf(splited, "thousand"))
            {
                parsedInt += ht[splited[i]];
            }
            if (i > Array.IndexOf(splited, "thousand"))
            {
                parsedInt += (ht[splited[i]])*1000;
            }
        }
        //hundred only case
        if (splited.Contains("hundred") && !splited.Contains("thousand")) //example: "one/two/four/eight...hundred and one/fortry-three"
        {
            if (i < indices[0])
            {
                parsedInt += ht[splited[i]];
            }
            if (i > indices[0])
            {
                parsedInt += (ht[splited[i]])*100;
            }
        }
        //below 100 case
        if (!splited.Contains("hundred") && !splited.Contains("thousand"))//example: "ninety-nine or nineteen..."
        {
            parsedInt += ht[splited[i]];
        }
        
    }

    Console.WriteLine("Индексы слова '{0}': {1}", "hundred", string.Join(", ", indices));
    //"hundred" indices search func
    List<int> FindAllIndices(string[] array, string value)
    {
        List<int> indices = new List<int>();

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == value)
            {
                indices.Add(i);
            }
        }

        return indices;
    }
    
    
    Console.WriteLine(parsedInt);
}

ParseInt("nine hundred and eighty-five thousand seven hundred and thirty-two"); //985732
ParseInt("one hundred"); //100
ParseInt("twenty-one"); //21
ParseInt("thirty-six thousand twenty-one"); //36021
ParseInt("six thousand nine hundred and one"); //6901
ParseInt("seven hundred eighty-five thousand twelve"); //785012
ParseInt("one million");
        