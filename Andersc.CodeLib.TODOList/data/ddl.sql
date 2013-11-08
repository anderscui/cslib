-- for sqlite version

CREATE TABLE "main"."todo_status" (
    "id" INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
    "key" TEXT NOT NULL,
    "value" TEXT NOT NULL
);

CREATE TABLE "main"."todo_priority" (
    "id" INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
    "key" TEXT NOT NULL,
    "value" TEXT NOT NULL
);

CREATE TABLE "main"."todo_category" (
    "id" INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
    "name" TEXT NOT NULL,
    "desc" TEXT,
	"color" TEXT
);

CREATE TABLE "main"."todo" (
    "id" INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
    "subject" TEXT NOT NULL,
    "memo" TEXT,
    "start_date" TEXT NOT NULL,
    "end_date" TEXT,
    "status" INTEGER NOT NULL,
    "priority" INTEGER NOT NULL,
    "progress" INTEGER NOT NULL,
    "reminder" INTEGER,
    "category_id" INTEGER
);

