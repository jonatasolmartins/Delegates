namespace Aula_9;

//We call void delegates Action 
delegate void ToUpperString(string message);
delegate void ToLowerString(string message);
delegate void ToUpperStringAction();
//We call delegates that return something Func
delegate string ToUpperReturn(string message);
//We can use generic delegates
delegate string ToUpperReturnGenericResponseState<T>(T responseState) where T : ResponseState;
//We can use generic delegates with constraints and covariance
delegate U ToUpperReturnGenericCovariant<in T, out U>(T item);
delegate U ToUpperReturnGeneric<T, U>(T item);
public class Delegates
{
    public static void Run()
    {
        //If the signature of the method matches the signature of the delegate we can assign the method to the delegate
        ToLowerString toLowerString = ToLowerString;
        
        ToLowerString toLowerString2 = (string message) => Console.WriteLine(message.ToLower());
        ToUpperString toUpperString3 = new ToUpperString(ToLowerString);
        ToUpperString toUpperString4 = new ToUpperString((string message) => Console.WriteLine(message.ToLower()));
        
        /* We can nest delegates inside of each other
         * ToUpperString holds a reference to the method ToUpperString and the anonymous method below
         * When we call toUpperString("Hello World") it will execute both methods in order
         * If we want to remove a method from the delegate we can use the -= operator
         * For example: toUpperString -= ToUpperString;
        */
        ToUpperString toUpperString = ToUpperString;
        toUpperString += (string message = "Test") => Console.WriteLine(message.ToUpper());
        ToUpperReturn toUpperReturn = (string message) => message.ToUpper();
        
        /*
         * If a exception is thrown in a delegate, the execution will stop and the exception will be thrown
         * The others methods in the delegate will not be executed
         * Unless the exception is handled in the delegate that throws the exception
         */
        ToUpperStringAction toUpperStringAction = () => Console.WriteLine("First");
        toUpperStringAction += () => Console.WriteLine("Second");
        toUpperStringAction += () => Console.WriteLine("Third");

        toUpperStringAction();
        var returnn = toUpperReturn("Return Hello World");
        Console.WriteLine(returnn);
        toLowerString("Hello World");
        toUpperString("Hello World");
        
        ToUpperReturnGenericResponseState<ResponseState> toUpperReturnGeneric = (ResponseState responseState) => responseState.Message.ToUpper();

        Console.WriteLine(toUpperReturnGeneric(new ResponseState() {Message = "Hello from generic"}));
        
        ToUpperReturnGeneric<ResponseState, string> toUpperReturnGenericString = (ResponseState responseState) => responseState.Message.ToUpper();
        Console.WriteLine(toUpperReturnGenericString(new ResponseState() {Message = "Without constraints"}));
        
        ToUpperReturnGeneric<ResponseState2, int> toUpperReturnGenericInt = (ResponseState2 responseState) => responseState.Message.ToUpper().Length;
        Console.WriteLine(toUpperReturnGenericInt(new ResponseState2() {Message = "Without constraints 2"}));
        
        InvokeActionWithoutParameter(() => Console.WriteLine("From InvokeActionWithoutParameter"));
        InvokeActionWithParamter((string mensage = "InvokeActionWithParamter") => Console.WriteLine($"From {mensage}"));
        Console.WriteLine(InvokeFunctionWithoutParameter(() => "InvokeFunctionWithoutParameter"));
        Console.WriteLine(InvokeFunctionWithParameter((string mensage = "InvokeFunctionWithParameter") => mensage));
    }
    
    public static void ToUpperString(string message)
    {
        Console.WriteLine(message.ToUpper());
    }
    
    public static void ToLowerString(string message)
    {
        Console.WriteLine(message.ToLower());
    }
    
    public string ToUpperReturn(string message)
    {
        return message.ToUpper();
    }
    
    public static void InvokeActionWithoutParameter(Action action)
    {
        action();
    }
    
    public static void InvokeActionWithParamter(Action<string> action)
    {
        action("From InvokeActionWithParamter");
    }
    
    public static string InvokeFunctionWithoutParameter(Func<string> func)
    {
        return func();
    }

    public static string InvokeFunctionWithParameter(Func<string, string> func)
    {
        return func("From InvokeFunctionWithParameter");
    }
}

public class BaseResponseState
{
    public string Message { get; set; }
}
public class ResponseState : BaseResponseState
{
    public string Message { get; set; }
}

public class ResponseState2 : BaseResponseState
{
    public string Message { get; set; }
}