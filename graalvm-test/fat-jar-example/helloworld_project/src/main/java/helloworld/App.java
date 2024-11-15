package helloworld;

import java.util.*;
import java.io.*;
import javax.mail.*;
import javax.mail.internet.*;
import javax.activation.*;

public class App {
    public static void main(String[] args) {
	    String smtpHostServer = "exmail.mtsu.edu";
		String port = "25";
	    String from = "besmith@mtsu.edu";
	    String to = "besmith@mtsu.edu";
	    String subject = "testing from java";
	    String message = "testing 1, 2, 3";
        boolean debug = false;

	    Properties props = System.getProperties();

	    props.put("mail.smtp.host", smtpHostServer);
		props.put("mail.smtp.port", port);
		props.put("mail.smtp.auth", "false");

	    Session session = Session.getInstance(props, null);
		session.setDebug(debug);

      try {
         MimeMessage msg = new MimeMessage(session);
         msg.setFrom(new InternetAddress(from));
         InternetAddress[] address = {new InternetAddress(to)};
         msg.setRecipients(Message.RecipientType.TO, address);
         msg.setSubject(subject);

         MimeBodyPart bodyPart = new MimeBodyPart();
         bodyPart.setText(message);

         Multipart mp = new MimeMultipart();
         mp.addBodyPart(bodyPart);
         msg.setContent(mp);

         msg.setSentDate(new Date());
         Transport.send(msg);
      } catch (MessagingException e) {
         e.printStackTrace();
         Exception e2 = null;
         if ((e2 = e.getNextException()) != null)
           e2.printStackTrace();
      }
    }
}
