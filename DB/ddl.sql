create table USERS
(
    USER_ID  NUMBER generated as identity,
    PASSWORD VARCHAR2(100)
)
/

create unique index PK_USER_ID
    on USERS (USER_ID)
/

alter table USERS
    add constraint USERS_PK_USER_ID
        primary key (USER_ID)
/

create trigger TRG_USERS
    after insert or delete
    on USERS
    for each row
DECLARE
BEGIN
    CASE
        WHEN INSERTING THEN
            INSERT INTO user_log
            values ('Add user', :new.USER_ID, 0, current_timestamp);

        WHEN DELETING THEN
            INSERT INTO user_log
            values ('Ban user', :old.USER_ID, 0, current_timestamp);
        END CASE;

end;
/

create table USER_PROFILE
(
    USER_ID  NUMBER not null
        constraint FK_USER_ID
            references USERS
                on delete cascade,
    NICKNAME VARCHAR2(255),
    EMAIL    VARCHAR2(255)
)
/

create table ROLE
(
    ROLE_ID   NUMBER not null,
    ROLE_NAME VARCHAR2(100)
)
/

create unique index PK_ROLE_ID
    on ROLE (ROLE_ID)
/

alter table ROLE
    add constraint ROLE_PK_ROLE_ID
        primary key (ROLE_ID)
/

create table USER2ROLE
(
    USER_ID NUMBER not null
        constraint USER2ROLE_FK_USER_ID
            references USERS
                on delete cascade,
    ROLE_ID NUMBER not null
        constraint USER2ROLE_FK_ROLE_ID
            references ROLE
)
/

create trigger TRG_USER2ROLE
    after update
    on USER2ROLE
    for each row
DECLARE
BEGIN
    CASE
        WHEN UPDATING THEN
            INSERT INTO user_log
            values ('Change user role', :new.USER_ID, :new.ROLE_ID, current_timestamp);

        END CASE;
end;
/

create table SRC_CUP
(
    YEAR           VARCHAR2(50),
    COUNTRY        VARCHAR2(50),
    WINNER         VARCHAR2(50),
    RUNNERS_UP     VARCHAR2(50),
    THIRD          VARCHAR2(50),
    FOURTH         VARCHAR2(50),
    GOALSSCORED    VARCHAR2(50),
    QUALIFIEDTEAMS VARCHAR2(50),
    MATCHESPLAYED  VARCHAR2(50),
    ATTENDANCE     VARCHAR2(50)
)
/

create table SRC_PLAYER
(
    ROUNDID       VARCHAR2(50),
    MATCHID       VARCHAR2(50),
    TEAM_INITIALS VARCHAR2(50),
    COACH_NAME    VARCHAR2(50),
    LINE_UP       VARCHAR2(50),
    SHIRT_NUMBER  VARCHAR2(50),
    PLAYER_NAME   VARCHAR2(50),
    POSITION      VARCHAR2(50),
    EVENT         VARCHAR2(50)
)
/

create table SRC_MATCH
(
    YEAR                 VARCHAR2(100),
    DATETIME             VARCHAR2(100),
    STAGE                VARCHAR2(100),
    STADIUM              VARCHAR2(100),
    CITY                 VARCHAR2(100),
    HOME_TEAM_NAME       VARCHAR2(100),
    HOME_TEAM_GOALS      VARCHAR2(100),
    AWAY_TEAM_GOALS      VARCHAR2(100),
    AWAY_TEAM_NAME       VARCHAR2(100),
    WIN_CONDITIONS       VARCHAR2(100),
    ATTENDANCE           VARCHAR2(100),
    HALF_TIME_HOME_GOALS VARCHAR2(100),
    HALF_TIME_AWAY_GOALS VARCHAR2(100),
    REFEREE              VARCHAR2(100),
    ASSISTANT_1          VARCHAR2(100),
    ASSISTANT_2          VARCHAR2(100),
    ROUNDID              VARCHAR2(100),
    MATCHID              VARCHAR2(100),
    HOME_TEAM_INITIALS   VARCHAR2(100),
    AWAY_TEAM_INITIALS   VARCHAR2(100)
)
/

create table TEAM
(
    TEAM_ID      NUMBER generated as identity
        constraint TEAM_PK_TOURNAMENT_ID
            primary key,
    TEAM_NAME    VARCHAR2(100),
    TEAM_INITIAL VARCHAR2(100)
)
/

create table STADIUM
(
    STADIUM_ID   NUMBER generated as identity
        constraint STADIUM_PK_TOURNAMENT_ID
            primary key,
    STADIUM_NAME VARCHAR2(100),
    CITY         VARCHAR2(100)
)
/

create table TOURNAMENT
(
    TOURNAMENT_ID   NUMBER generated as identity
        constraint TOURNAMENT_PK_TOURNAMENT_ID
            primary key,
    YEAR            NUMBER,
    COUNTRY         VARCHAR2(50),
    TOURNAMENT_NAME VARCHAR2(50),
    WINNER_ID       NUMBER
        constraint FK_WINNER
            references TEAM,
    RUNNERS_UP_ID   NUMBER
        constraint FK_RUNNERS_UP
            references TEAM,
    THIRD_ID        NUMBER
        constraint FK_THIRD
            references TEAM,
    FOURTH_ID       NUMBER
        constraint FK_FOURTH
            references TEAM,
    GOALS_SCORED    NUMBER,
    QUALIFIED_TEAMS NUMBER,
    MATCHES_PLAYED  NUMBER
)
/

create table MATCH
(
    MATCH_ID        NUMBER not null
        constraint MATCH_PK_MATCH_ID
            primary key,
    TOURNAMENT_ID   NUMBER not null
        constraint FK_TOURNAMENT
            references TOURNAMENT,
    DATETIME        TIMESTAMP(6),
    STAGE           VARCHAR2(100),
    STADIUM_ID      NUMBER
        constraint FK_STADIUM
            references STADIUM,
    HOME_TEAM_ID    NUMBER
        constraint FK_HOME_TEAM
            references TEAM,
    HOME_TEAM_GOALS NUMBER,
    AWAY_TEAM_ID    NUMBER
        constraint FK_AWAY_TEAM
            references TEAM,
    AWAY_TEAM_GOALS NUMBER,
    WIN_CONDITIONS  VARCHAR2(100),
    REFEREE         VARCHAR2(100)
)
/

create index MATCH_DATETIME_INDEX
    on MATCH (DATETIME)
/

create table REFEREE
(
    MATCH_ID               NUMBER
        constraint REFEREE_FK_MATCH
            references MATCH,
    REFEREE_ID             NUMBER generated as identity
        constraint REFEREE_PK_REFEREE_ID
            primary key,
    REFEREE_NAME           VARCHAR2(100),
    ASSISTANT_1_REFEREE_ID NUMBER,
    ASSISTANT_2_REFEREE_ID NUMBER
)
/

create table COACH
(
    COACH_ID   NUMBER generated as identity
        constraint COACH_PK_COACH_ID
            primary key,
    COACH_NAME VARCHAR2(100)
)
/

create table PLAYER
(
    PLAYER_ID    NUMBER generated as identity
        constraint PLAYER_PK_PLAYER_ID
            primary key,
    PLAYER_NAME  VARCHAR2(100),
    SHIRT_NUMBER VARCHAR2(50),
    COACH_ID     NUMBER
        constraint PLAYER_FK_COACH
            references COACH
)
/

create table PLAYER2MATCH
(
    PLAYER_ID NUMBER
        constraint FK_PLAYER
            references PLAYER,
    MATCH_ID  NUMBER
        constraint FK_MATCH
            references MATCH,
    TEAM_ID   NUMBER
        constraint FK_TEAM
            references TEAM
)
/

create table USER_LOG
(
    ACTION    VARCHAR2(100),
    USER_ID   NUMBER,
    ROLE_ID   NUMBER,
    INSERT_DT TIMESTAMP(6) default CURRENT_TIMESTAMP
)
/

create PROCEDURE get_user(
    p_userid IN NUMBER,
    out_user OUT SYS_REFCURSOR
) AS
BEGIN
    OPEN out_user FOR SELECT user_id
                           , user_profile.email
                           , user_profile.nickname
                           , role_id
                           , role_name
                      FROM user_profile
                               inner join user2role using (user_id)
                               inner join role using (role_id)
                      WHERE user_id = p_userid;

EXCEPTION
    WHEN OTHERS THEN
        out_user := NULL;
END get_user;
/

create PROCEDURE post_register_user(
    p_nickname IN user_profile.nickname%TYPE,
    p_email IN user_profile.email%TYPE,
    p_password IN users.password%TYPE
) IS
    ex_email EXCEPTION;
    PRAGMA exception_init ( ex_email, -20001 );
    v_userid      NUMBER;
    v_unique_flag NUMBER;
BEGIN
    SELECT count(*)
    INTO v_unique_flag
    FROM user_profile
    WHERE email = p_email;

    IF v_unique_flag > 0 THEN
        raise_application_error(-20001, 'email is not unique');
    END IF;

    INSERT INTO users (password) VALUES (p_password) RETURNING user_id INTO v_userid;


    INSERT INTO user_profile
    VALUES (v_userid, p_nickname, p_email);

    INSERT INTO user2role
    VALUES (v_userid, 0);

END post_register_user;
/

create PROCEDURE get_log_in(
    p_email IN user_profile.email%TYPE,
    p_password IN users.password%TYPE,
    out_user_id OUT SYS_REFCURSOR
) IS
    v_user_id NUMBER;
BEGIN
    BEGIN
        SELECT user_id
        INTO v_user_id
        FROM users
                 INNER JOIN user_profile USING (user_id)
        WHERE email = p_email
          AND password = p_password;

    EXCEPTION
        WHEN no_data_found THEN
            v_user_id := NULL;
    END;

    OPEN out_user_id FOR SELECT v_user_id AS user_id
                         FROM dual;

END get_log_in;
/

create PROCEDURE get_tournaments(
    p_year IN TOURNAMENT.YEAR%TYPE default null,
    out_tournaments OUT SYS_REFCURSOR
) AS
BEGIN
    OPEN out_tournaments FOR select TOURNAMENT_ID
                                  , YEAR
                                  , COUNTRY
                                  , TOURNAMENT_NAME
                                  , WINNER_ID
                                  , T1.TEAM_NAME    as WINNER_TEAM_NAME
                                  , T1.TEAM_INITIAL as WINNER_TEAM_INITIAL
                                  , RUNNERS_UP_ID
                                  , T2.TEAM_NAME    as RUNNERS_UP_TEAM_NAME
                                  , T2.TEAM_INITIAL as RUNNERS_UP_TEAM_INITIAL
                                  , THIRD_ID
                                  , T3.TEAM_NAME    as THIRD_TEAM_NAME
                                  , T3.TEAM_INITIAL as THIRD_TEAM_INITIAL
                                  , FOURTH_ID
                                  , T4.TEAM_NAME    as FOURTH_TEAM_NAME
                                  , T4.TEAM_INITIAL as FOURTH_TEAM_INITIAL
                                  , GOALS_SCORED
                                  , QUALIFIED_TEAMS
                                  , MATCHES_PLAYED
                             from TOURNAMENT tr
                                      join TEAM T1 on tr.WINNER_ID = T1.TEAM_ID
                                      join TEAM T2 on tr.RUNNERS_UP_ID = T2.TEAM_ID
                                      join TEAM T3 on tr.THIRD_ID = T3.TEAM_ID
                                      join TEAM T4 on tr.FOURTH_ID = T4.TEAM_ID
                             where (YEAR = p_year or p_year is null)
                             order by YEAR;

END get_tournaments;
/

create PROCEDURE get_matches(
    p_tournament_id NUMBER,
    out_matches OUT SYS_REFCURSOR
) AS
BEGIN
    OPEN out_matches FOR select M.MATCH_ID
                              , TOURNAMENT_ID
                              , DATETIME
                              , S.STADIUM_NAME
                              , S.CITY
                              , STAGE
                              , HOME_TEAM_ID
                              , T2.TEAM_NAME       HOME_TEAM_NAME
                              , T2.TEAM_INITIAL    HOME_TEAM_INITIAL
                              , HOME_TEAM_GOALS
                              , AWAY_TEAM_ID
                              , T.TEAM_NAME        AWAY_TEAM_NAME
                              , T.TEAM_INITIAL     AWAY_TEAM_INITIAL
                              , AWAY_TEAM_GOALS
                              , WIN_CONDITIONS
                              , r1.REFEREE_NAME
                              , r2.REFEREE_NAME as ASSISTANT_1
                              , r3.REFEREE_NAME as ASSISTANT_2
                         from MATCH M
                                  inner join STADIUM S on S.STADIUM_ID = M.STADIUM_ID
                                  inner join TEAM T on T.TEAM_ID = M.AWAY_TEAM_ID
                                  inner join TEAM T2 on T2.TEAM_ID = M.HOME_TEAM_ID
                                  inner join REFEREE r1 on r1.MATCH_ID = M.MATCH_ID
                             and ASSISTANT_1_REFEREE_ID is not null
                             and ASSISTANT_2_REFEREE_ID is not null
                                  inner join REFEREE r2 on r1.MATCH_ID = r2.MATCH_ID
                             and r1.ASSISTANT_1_REFEREE_ID = r2.REFEREE_ID
                                  inner join REFEREE r3 on r1.MATCH_ID = r3.MATCH_ID
                             and r1.ASSISTANT_2_REFEREE_ID = r3.REFEREE_ID
                         where TOURNAMENT_ID = p_tournament_id
                         order by DATETIME;
END get_matches;
/

create PROCEDURE get_teams(
    p_tournament_id NUMBER,
    out_teams OUT SYS_REFCURSOR
) AS
BEGIN
    OPEN out_teams FOR select distinct TOURNAMENT_ID
                                     , T.TEAM_ID
                                     , T.TEAM_NAME
                                     , T.TEAM_INITIAL
                                     , count(distinct p.PLAYER_ID)
                                             over (partition by TOURNAMENT_ID,p.TEAM_ID) as TEAM_COUNT
                                     , p.PLAYER_ID
                                     , PLAYER_NAME
                                     , SHIRT_NUMBER
                                     , COACH_NAME
                       from PLAYER2MATCH p
                                inner join MATCH m on p.MATCH_ID = m.MATCH_ID
                                inner join PLAYER pl on p.PLAYER_ID = pl.PLAYER_ID
                                inner join COACH c on pl.COACH_ID = c.COACH_ID
                                inner join TEAM T on T.TEAM_ID = p.TEAM_ID
                       where TOURNAMENT_ID = p_tournament_id
                       order by TOURNAMENT_ID, T.TEAM_ID;
END get_teams;
/

create PROCEDURE post_change_role(
    p_user_id IN USERS.USER_ID%TYPE,
    p_role_id IN ROLE.ROLE_ID%TYPE
) IS
BEGIN
    UPDATE USER2ROLE
    set ROLE_ID = p_role_id
    where USER_ID = p_user_id;

END post_change_role;
/

create PROCEDURE post_del_user(
    p_user_id IN USERS.USER_ID%TYPE
) IS
BEGIN
    delete
    from USERS
    where USER_ID = p_user_id;

END post_del_user;
/

create PROCEDURE get_all_users(
    out_users OUT SYS_REFCURSOR
) AS
BEGIN
    OPEN out_users FOR
        SELECT user_id
             , user_profile.email
             , user_profile.nickname
             , role_id
             , role_name
        FROM user_profile
                 inner join user2role using (user_id)
                 inner join role using (role_id);
EXCEPTION
    WHEN
        OTHERS THEN
        out_users := NULL;
END get_all_users;
/

