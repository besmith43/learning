import Vapor

func routes(_ app: Application) throws {
    app.get { req async in
        "It works!"
    }

    app.get("hello") { req async -> String in
        "Hello, world!"
    }
    
    app.get("calcFib", ":fibnumber") { req async -> Int in
        guard let num = req.parameters.get("fibnumber", as: Int.self) else {
            return 0;
        }
        
        if (num == 1)
        {
            return 0;
        }
        
        if (num == 2)
        {
            return 1;
        }
        
        var x = 1;
        var y = 1;
        var sum = 0;
        
        for _ in 2...num-1
        {
            sum = x + y;
            x = y;
            y = sum;
        }
        
        return sum;
    }
}
