FROM eclipse-temurin:25-jre
WORKDIR /app
COPY build/libs/*.war /app/office-time.war
EXPOSE 8080
ENTRYPOINT ["java", "-jar", "/app/office-time.war"]
