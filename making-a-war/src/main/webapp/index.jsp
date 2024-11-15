<%@ page import="hello.Hello" %>
<html>
    <head>
        <title>Hello</title>
        <link rel="stylesheet" href="webjars/font-awesome/6.5.2/css/all.min.css" />
        <link href="https://cdn.jsdelivr.net/npm/daisyui@4.12.14/dist/full.min.css" rel="stylesheet" type="text/css" />
        <script src="https://cdn.tailwindcss.com"></script>
    </head>
    <body>
        <h1>Hello World!</h1>
        <p>
            <%
                out.println(Hello.Greeting());
            %>
        </p>

        <p>Do I have permission?
            <%
                out.println(Hello.PermissionCheck());
            %>
        </p>
        <p>just putting in something to say that I made a change</p>

        <button id="what" class="button is-primary"><i class="fa-solid fa-comment"></i></button>

        <div id="replaceMe">
            <button hx-get="/making_a_war_war_exploded/newbie.jsp" hx-trigger="click" hx-target="#replaceMe">Click Me Please</button>
        </div>

        <div id="modals" hx-get="/making_a_war_war_exploded/MyMenu.jsp" hx-trigger="load" hx-target="#modals"></div>

        <a href="MethodTest.jsp">Check Methods</a>


        <div id="footer" hx-get="/making_a_war_war_exploded/footer.jsp" hx-trigger="load" hx-target="#footer"></div>

        <script src="webjars/jquery/3.7.1/dist/jquery.js"></script>
        <script src="webjars/htmx.org/2.0.3/dist/htmx.js"></script>
        <script src="webjars/sweetalert2/11.14.3/dist/sweetalert2.all.js"></script>

        <script>
            $("#what").on("click", function() {
                Swal.fire({
                    title: "Good job!",
                    text: "You clicked the button!",
                    icon: "success"
                });
            });
        </script>

    </body>
</html>
