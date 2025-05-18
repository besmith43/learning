import java.io.IOException;
import java.io.BufferedReader;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.annotation.WebServlet;
import com.google.gson.Gson;

@WebServlet("/api")
public class MyServlet extends HttpServlet {

    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {

        System.out.println("doGet called");

        // Sample data (replace with your actual data)
        MyData data = new MyData("example", 123);

        // Convert to JSON using Gson
        Gson gson = new Gson();
        String json = gson.toJson(data);

        // Set response content type
        response.setContentType("application/json");
        response.setCharacterEncoding("UTF-8");

        // Write JSON to response
        response.getWriter().write(json);
        response.getWriter().flush();
    }


    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {

        System.out.println("doPost called");

        var headerNames = request.getHeaderNames();

        while (headerNames.hasMoreElements()) {
            var name = headerNames.nextElement();
            System.out.println("Request Header Name: " + name);
        }

        var requestReader = request.getReader();
        String line;
        while ((line = requestReader.readLine()) != null) {
            System.out.println("Request Body: " + line);
        }

        // Sample data (replace with your actual data)
        MyData data = new MyData("post example", 456);

        // Convert to JSON using Gson
        Gson gson = new Gson();
        String json = gson.toJson(data);

        // Set response content type
        response.setContentType("application/json");
        response.setCharacterEncoding("UTF-8");

        // Write JSON to response
        response.getWriter().write(json);
        response.getWriter().flush();
    }
}

