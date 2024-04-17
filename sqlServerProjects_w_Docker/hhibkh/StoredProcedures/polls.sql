CREATE PROCEDURE createPoll
    @question VARCHAR(100),
    @answers VARCHAR(1000)
    AS
    BEGIN
        INSERT INTO poll (question) VALUES (@question);
        DECLARE @poll_id INT;
        SET @poll_id = SCOPE_IDENTITY();
        DECLARE @xml XML;
        SET @xml = CAST(('<A>'+REPLACE(@answers, ',', '</A><A>')+'</A>') AS XML);
        INSERT INTO pollAnswers (poll_id, answer)
        SELECT @poll_id, T.c.value('.', 'VARCHAR(100)') AS answer
        FROM @xml.nodes('/A') AS T(c);
    END;