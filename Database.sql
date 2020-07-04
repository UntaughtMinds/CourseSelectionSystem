drop table if exists course_db;

drop table if exists student_course_maping;

drop table if exists student_db;

/*==============================================================*/
/* Table: course_db                                             */
/*==============================================================*/
create table course_db
(
   code                 int not null,
   name                 varchar(64),
   is_elective          int,
   credit               int,
   primary key (code)
);

alter table course_db comment '课程信息';

/*==============================================================*/
/* Table: student_course_maping                                 */
/*==============================================================*/
create table student_course_maping
(
   id                   int not null,
   student_id           int,
   course_id            int,
   primary key (id)
);

/*==============================================================*/
/* Table: student_db                                            */
/*==============================================================*/
create table student_db
(
   id                   int not null,
   name                 varchar(64),
   class                varchar(64),
   credit               int,
   primary key (id)
);

alter table student_db comment '学生信息';

alter table student_course_maping add constraint FK_Reference_1 foreign key (course_id)
      references course_db (code) on delete restrict on update restrict;

alter table student_course_maping add constraint FK_Reference_2 foreign key (student_id)
      references student_db (id) on delete restrict on update restrict;
