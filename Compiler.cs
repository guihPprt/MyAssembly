using System.Data.Common;
using System.IO;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace MyAssembly
{
    class Compiler
    {
        
        public static string path;
        string sep = "-------------------";
        public string[] code_lines;

        public bool is_goto = false;

        public int rep_value = 1;
        public int ant_ln = 0;
        public string cache = "cache.rom";
        public string text_cache = "text_cache.rom";

        public string block_cache = "block_cache.rom";
        static void Main(string[] args)
        {
            path = args[0];
            Compiler compiler = new Compiler();
            
        }

        public Compiler()
        {
            File.Delete("cache.rom");
            File.Create("cache.rom").Close();
            
            File.Delete(text_cache);
            File.Create(text_cache).Close();

            File.Delete(block_cache);
            File.Create(block_cache).Close();


            code_lines = File.ReadAllLines(path);
           
            int ln = 0;
            
            while(ln < code_lines.Length)
            {
                
                string line = code_lines[ln];
               
                try{
                    var split = line.Split(' ');
                    var opcode = split[0];

                    if (opcode == "PUT")
                    {
                        
                        File.AppendAllText(cache, split[1] + "\n");
                    }

                    if(opcode == "WRT")
                    {
                        var text = File.ReadAllLines(cache);
                        for(int i = 0; i < text.Length; i++)
                        {
                            if(i+1 == float.Parse(split[1]))
                            {
                                Console.WriteLine(text[i]);                                
                            }
                        }
                        
                    }


                    if(opcode == "SUM")
                    {
                        var text = File.ReadAllLines(cache);

                        float n1 = float.Parse(text[int.Parse(split[1])-1]);
                        float n2 = float.Parse(text[int.Parse(split[2])-1]);
                        float sum = n1+n2;
                        File.AppendAllText(cache, sum.ToString() + "\n");
                    }

                    if(opcode == "SUB")
                    {
                        var text = File.ReadAllLines(cache);

                        float n1 = float.Parse(text[int.Parse(split[1])-1]);
                        float n2 = float.Parse(text[int.Parse(split[2])-1]);
                        float sub = n1-n2;
                        File.AppendAllText(cache, sub.ToString() + "\n");
                    }

                    if(opcode == "DIV")
                    {
                        var text = File.ReadAllLines(cache);

                        float n1 = float.Parse(text[int.Parse(split[1])-1]);
                        float n2 = float.Parse(text[int.Parse(split[2])-1]);
                        float div = n1/n2;
                        File.AppendAllText(cache, div.ToString() + "\n");
                    }

                    if(opcode == "MUL")
                    {
                        var text = File.ReadAllLines(cache);

                        float n1 = float.Parse(text[int.Parse(split[1])-1]);
                        float n2 = float.Parse(text[int.Parse(split[2])-1]);
                        float mul = n1*n2;
                        File.AppendAllText(cache, mul.ToString() + "\n");
                    }
                

                    if(opcode == "{SEP}")
                    {
                        Console.WriteLine(sep);
                    }

                    if(opcode == "{CLS}")
                    {
                        Console.Clear();
                    }

                    if(opcode == "T.PUT")
                    {
                        if(code_lines[ln+1].Contains(">"))
                        {
                            if(code_lines[ln+1].Contains("<"))
                            {
                                File.AppendAllText(text_cache,code_lines[ln+1].Replace(">","").Replace("<","")+"\n");
                            }
                            else
                            {
                                Console.WriteLine("Error at line > "+(ln+1).ToString()+" <");
                                Console.WriteLine("Expected < to close a string");
                                Environment.Exit(0x02);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error at line > "+(ln+1).ToString()+" <");
                            Console.WriteLine("Expected > to indentify as string");
                            Environment.Exit(0x02);
                        }
                    }
                    if(opcode == "T.WRT")
                    {
                        var text = File.ReadAllLines(text_cache);
                        for(int i = 0; i < text.Length; i++)
                        {
                            if(i+1 == float.Parse(split[1]))
                            {
                                Console.WriteLine(text[i]);                                
                            }
                        }
                    }

                    if(opcode == "T.INP")
                    {
                        if(is_goto == false)
                        {
                            var text = Console.ReadLine();
                            File.AppendAllText(text_cache,text + "\n");
                        }
                        
                    }

                    if(opcode == "INP")
                    {
                        if(is_goto == false)
                        {
                            try{
                                float number = float.Parse(Console.ReadLine());
                                File.AppendAllText(cache,number.ToString() + "\n");
                            }
                            catch(Exception e)
                            {
                                Console.WriteLine("Please provide a Number");
                                Environment.Exit(0x01);
                            }
                        }
                    }

                    
                    
                    
                    if(opcode != "")
                    {
                        if(!is_goto)
                        {
                            if(opcode[0] == ':')
                            {
                                int pointer = ln;
                                bool finded = false;
                                File.AppendAllText(block_cache,(ln).ToString() + "\n");

                                while(!finded)
                                {
                                    ln++;

                                    if(code_lines[ln].Contains("END"))
                                    {
                                        finded = true;
                                    }
                                }

                            }
                        }
                    }
                

                    if(opcode == "END")
                    {
                        File.AppendAllText(block_cache,(ln).ToString() + "\n");

                        if(is_goto)
                        {
                            is_goto = false;
                            ln = ant_ln;
                            
                        }
                    }



                    if(opcode == "GOTO")
                    {
                        ant_ln = ln;

                        for(int i = 0; i < code_lines.Length; i++)
                        {
                            if(code_lines[i].Split(" ")[0] == ":" && code_lines[i].Split(" ")[1] == split[1])
                            {
                                ln = i;
                                is_goto = true;
                                
                                
                            }
                        }
                    }


                    if(opcode == "IF")
                    {
                        var text = File.ReadAllLines(cache);

                        string value1 = "";
                        string value2 = "'";
                        for(int i = 0; i < text.Length; i++)
                        {
                            if(i+1 == float.Parse(split[1]))
                            {
                                value1 = text[i];
                            }
                            if(i+1 == float.Parse(split[3]))
                            {
                                value2 = text[i];
                            }
                        }

                        switch(split[2])
                        {
                            case "EQ":
                                if(value1 == value2)
                                {
                                    ant_ln = ln;

                                    for(int i = 0; i < code_lines.Length; i++)
                                    {
                                        if(code_lines[i].Split(" ")[0] == ":" && code_lines[i].Split(" ")[1] == split[4])
                                        {
                                            ln = i;
                                            is_goto = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if(split[5] != "NO")
                                    {
                                        ant_ln = ln;

                                        for(int i = 0; i < code_lines.Length; i++)
                                        {
                                            if(code_lines[i].Split(" ")[0] == ":" && code_lines[i].Split(" ")[1] == split[5])
                                            {
                                                ln = i;
                                                is_goto = true;
                                            }
                                        }
                                    }
                                }

                            break;
                            case "LT":
                                if(float.Parse(value1) < float.Parse(value2))
                                {
                                    ant_ln = ln;

                                    for(int i = 0; i < code_lines.Length; i++)
                                    {
                                        if(code_lines[i].Split(" ")[0] == ":" && code_lines[i].Split(" ")[1] == split[4])
                                        {
                                            ln = i;
                                            is_goto = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if(split[5] != "NO")
                                    {
                                        ant_ln = ln;

                                        for(int i = 0; i < code_lines.Length; i++)
                                        {
                                            if(code_lines[i].Split(" ")[0] == ":" && code_lines[i].Split(" ")[1] == split[5])
                                            {
                                                ln = i;
                                                is_goto = true;
                                            }
                                        }
                                    }
                                }
                            break;
                            case "MT":
                                if(float.Parse(value1) > float.Parse(value2))
                                {
                                    ant_ln = ln;

                                    for(int i = 0; i < code_lines.Length; i++)
                                    {
                                        if(code_lines[i].Split(" ")[0] == ":" && code_lines[i].Split(" ")[1] == split[4])
                                        {
                                            ln = i;
                                            is_goto = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if(split[5] != "NO")
                                    {
                                        ant_ln = ln;

                                        for(int i = 0; i < code_lines.Length; i++)
                                        {
                                            if(code_lines[i].Split(" ")[0] == ":" && code_lines[i].Split(" ")[1] == split[5])
                                            {
                                                ln = i;
                                                is_goto = true;
                                            }
                                        }
                                    }
                                }
                            break;

                            case "LE":
                                if(float.Parse(value1) <= float.Parse(value2))
                                {
                                    ant_ln = ln;

                                    for(int i = 0; i < code_lines.Length; i++)
                                    {
                                        if(code_lines[i].Split(" ")[0] == ":" && code_lines[i].Split(" ")[1] == split[4])
                                        {
                                            ln = i;
                                            is_goto = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if(split[5] != "NO")
                                    {
                                        ant_ln = ln;

                                        for(int i = 0; i < code_lines.Length; i++)
                                        {
                                            if(code_lines[i].Split(" ")[0] == ":" && code_lines[i].Split(" ")[1] == split[5])
                                            {
                                                ln = i;
                                                is_goto = true;
                                            }
                                        }
                                    }
                                }
                            break;
                            case "ME":
                                if(float.Parse(value1) >= float.Parse(value2))
                                {
                                    ant_ln = ln;

                                    for(int i = 0; i < code_lines.Length; i++)
                                    {
                                        if(code_lines[i].Split(" ")[0] == ":" && code_lines[i].Split(" ")[1] == split[4])
                                        {
                                            ln = i;
                                            is_goto = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if(split[5] != "NO")
                                    {
                                        ant_ln = ln;

                                        for(int i = 0; i < code_lines.Length; i++)
                                        {
                                            if(code_lines[i].Split(" ")[0] == ":" && code_lines[i].Split(" ")[1] == split[5])
                                            {
                                                ln = i;
                                                is_goto = true;
                                            }
                                        }
                                    }
                                }
                            break;
                            case "DIF":
                            
                                if(float.Parse(value1) != float.Parse(value2))
                                {
                                    ant_ln = ln;

                                    for(int i = 0; i < code_lines.Length; i++)
                                    {
                                        if(code_lines[i].Split(" ")[0] == ":" && code_lines[i].Split(" ")[1] == split[4])
                                        {
                                            ln = i;
                                            is_goto = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if(split[5] != "NO")
                                    {
                                        ant_ln = ln;

                                        for(int i = 0; i < code_lines.Length; i++)
                                        {
                                            if(code_lines[i].Split(" ")[0] == ":" && code_lines[i].Split(" ")[1] == split[5])
                                            {
                                                ln = i;
                                                is_goto = true;
                                            }
                                        }
                                    }
                                }
                            break;
                        }
                        
                    }

                    if(opcode == "T.IF")
                    {
                        var text = File.ReadAllLines(text_cache);

                        string value1 = "";
                        string value2 = "";
                        for(int i = 0; i < text.Length; i++)
                        {
                            if(i+1 == float.Parse(split[1]))
                            {
                                value1 = text[i];
                            }
                            if(i+1 == float.Parse(split[3]))
                            {
                                value2 = text[i];
                            }
                        }

                        

                        switch(split[2])
                        {
                            case "EQ":
                                if(value1 == value2)
                                {
                                    ant_ln = ln;

                                    for(int i = 0; i < code_lines.Length; i++)
                                    {
                                        if(code_lines[i].Split(" ")[0] == ":" && code_lines[i].Split(" ")[1] == split[4])
                                        {
                                            ln = i;
                                            is_goto = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if(split[5] != "NO")
                                    {
                                        ant_ln = ln;

                                        for(int i = 0; i < code_lines.Length; i++)
                                        {
                                            if(code_lines[i].Split(" ")[0] == ":" && code_lines[i].Split(" ")[1] == split[5])
                                            {
                                                ln = i;
                                                is_goto = true;
                                            }
                                        }
                                    }
                                }

                            break;

                            case "DI":
                                if(value1 != value2)
                                {
                                    ant_ln = ln;

                                    for(int i = 0; i < code_lines.Length; i++)
                                    {
                                        if(code_lines[i].Split(" ")[0] == ":" && code_lines[i].Split(" ")[1] == split[4])
                                        {
                                            ln = i;
                                            is_goto = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if(split[5] != "NO")
                                    {
                                        ant_ln = ln;

                                        for(int i = 0; i < code_lines.Length; i++)
                                        {
                                            if(code_lines[i].Split(" ")[0] == ":" && code_lines[i].Split(" ")[1] == split[5])
                                            {
                                                ln = i;
                                                is_goto = true;
                                            }
                                        }
                                    }
                                }

                            break;
                        }
                    }

                    if(opcode == "RST")
                    {
                        ln = 1;
                        File.Delete("cache.rom");
                        File.Create("cache.rom").Close();
                        
                        File.Delete(text_cache);
                        File.Create(text_cache).Close();

                        File.Delete(block_cache);
                        File.Create(block_cache).Close();

                        
                    }

                    if(opcode == "REP")
                    {
                        if(is_goto == false)
                        {
                            if(code_lines[ln+1] != "CON")
                            {
                                Console.WriteLine($"Error at line > {ln+1} <");
                                Console.WriteLine("Expected CON to continue");

                                Environment.Exit(0x04);
                            }
                            if(rep_value < float.Parse(split[1]))
                            {
                                ant_ln = ln-1;
                            }
                            else
                            {
                                ant_ln = ln;
                            }
                            rep_value++;
                            

                            for(int i = 0; i < code_lines.Length; i++)
                            {
                                if(code_lines[i].Split(" ")[0] == ":" && code_lines[i].Split(" ")[1] == split[2])
                                {
                                    ln = i;
                                    is_goto = true;
                                }
                            }
                        }
                    }

                    if(opcode == "CON")
                    {
                        rep_value = 1;
                    }
                    

                    //Console.WriteLine(ln);

                    ln++;
                }
                catch(Exception e)
                {
                    Console.WriteLine("Error at line > "+ln.ToString()+" <");
                    //Console.WriteLine(e);
                    Environment.Exit(0x00);
                }
            }
            
        }
        
        
    }
}