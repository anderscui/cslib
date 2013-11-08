CREATE TABLE "user" (
    "id" INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
    "name" TEXT NOT NULL,
    "birth" TEXT,
    "memo" TEXT
);
CREATE TABLE sqlite_sequence(name,seq);
CREATE TABLE "runner_cmds" (
    "id" INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
    "cmd_path" TEXT NOT NULL,
    "paras" TEXT,
    "bias" TEXT,
    "start_path" TEXT,
    "win_state" INTEGER,
    "memo" TEXT
);
CREATE TABLE "runner_webby" (
    "id" INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
    "name" TEXT NOT NULL,
    "url_format" TEXT NOT NULL,
    "created_date" TEXT NOT NULL
);
