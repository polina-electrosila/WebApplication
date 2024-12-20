namespace WebApplication.Data;

public class Response
{
    public String Message{get;set;} = string.Empty; // Сообщение (текст отклика)
    public object Data{get;set;}
    
    public void PrintResults()
    {
        Console.Out.Write(Message + "\n");
    }
}