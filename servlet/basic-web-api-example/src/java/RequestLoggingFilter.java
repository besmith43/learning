import javax.servlet.*;

import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.annotation.WebFilter;
import java.io.IOException;

@WebFilter("/*")
public class RequestLoggingFilter implements Filter {

    @Override
    public void init(FilterConfig filterConfig) throws ServletException {
        // Initialization logic
    }

    @Override
    public void doFilter(ServletRequest request, ServletResponse response, FilterChain chain) throws IOException, ServletException {
        // Log request details
        System.out.println("Logging Filter Request: " + request.getRemoteAddr() + " " + ((HttpServletRequest) request).getMethod() + " " + ((HttpServletRequest) request).getRequestURI());

        // Pass request to the next filter or servlet
        chain.doFilter(request, response);

        // Log response status
        System.out.println("Logging Filter Response: " + ((HttpServletResponse) response).getStatus());
    }

    @Override
    public void destroy() {
        // Cleanup logic
    }
}

