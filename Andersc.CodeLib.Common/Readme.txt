CommonUtil 配置项说明

1. 页面校验
	PageValidatorXmlPath ： XML文件路径，如 ~/PageValidator.xml， 不要用绝对路径。
	PageValidatorSchemaPath ： Schema文件路径， 如 ~/PageValidator.xsd， 不要用绝对路径。
	
2. IBatis配置项

	代码生成模板：Statements，表需要含有主键，如果含有自增长主键，还需要自行处理。


3. 加密解密
	PublicKeyFile ： 公钥存放文件绝对路径。
	PrivateKeyFile ： 密钥存放文件绝对路径。
	
	
4. log4net




Web Forms
编辑页面的步骤分割：获取QueryString，应用校验文件，初始化页面控件->提交：校验，提交，反馈，刷新主页面。

