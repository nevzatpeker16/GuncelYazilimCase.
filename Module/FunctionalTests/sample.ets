;#DropDB Solution1EasyTest

#Application Solution1Win
#Application Solution1Web

*FillForm
 User Name = Admin
*Action Log In

#IfDef Solution1Win

*Action About...
 
*Action OK

#EndIf

#IfDef Solution1Web
*Action Navigation(My Details)
#EndIf
