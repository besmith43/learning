I would like you to make a website to allow employee's to input how they spent their time in the office.

tech stack:

- java 25
- gradle (kts)
- spring boot
- reactjs
- postgresql
- docker compose
- playwright
- junit

Project Requirements:

- users need to use basic auth to sign in
- there needs to be 3 classes of users
    - site admins
    - managers
    - employees
- users need to be able to input standard hours (with a complete submission totaling exactly 8 hours) and overtime (extended hours) which need manager approval before counting
- users need to be able to search their previous entries, and request a single day's to be deleted (which will only able aftermanagerial approval)
- managers need to be able to see the inputs of each of their employees and download a CSV copy
- user entries consist of the following data points:
    - user_id (to be pulled from the current user's auth)
    - working date (should be populated onto the page server side for standard hours, and selectable for extended hours)
    - team
    - task (be sure to make paid time off options available such as annual leave and sick leave)
    - comments
    - hours
- there should be unit tests, integration tests, and e2e testing using junit and playwright where reasonable

project utility scripts:

- build.sh
    - use gradle to build the project
    - run any and all test suites
- run.sh
    - execute `build.sh`
    - run project via docker compose and populate the postgresql database with a default starting point
- publish.sh
    - execute `build.sh`
    - use gradle to produce a war file to be deployed into a tomcat 11 runtime
- test.sh
    - run all unit tests, integration tests, and e2e tests

