# Key methods of HttpServlet

- doGet(HttpServletRequest request, HttpServletResponse response):
Handles HTTP GET requests.

- doPost(HttpServletRequest request, HttpServletResponse response):
Handles HTTP POST requests.

- doPut(HttpServletRequest request, HttpServletResponse response):
Handles HTTP PUT requests.

- doDelete(HttpServletRequest request, HttpServletResponse response):
Handles HTTP DELETE requests.

- service(HttpServletRequest request, HttpServletResponse response):
Dispatches the request to the appropriate doXXX method based on the HTTP method.
It is generally not overridden unless custom handling of all HTTP methods is needed.

- init(ServletConfig config):
Initializes the servlet.
Called by the servlet container when the servlet is first loaded.

- destroy():
Destroys the servlet.
Called by the servlet container when the servlet is being taken out of service.

