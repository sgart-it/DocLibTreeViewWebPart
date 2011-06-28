OS versione: {s:osversion}
<br />
serverName: {s:servername}
<br />
ServerIP: {s:serverip}
<br />
ClientName: {s:clientname}
<br />
ClientNameFull: {s:clientnamefull}
<br />
ClientIP: {s:clientip}
<br />

Title: {s:title}
<br />
Description: {s:description}
<br />
Lcid: {s:lcid}
<br />
WebUrl: {s:weburl}
<br />
WebUrlFull: {s:weburlfull}
<br />
LayoutsUrl: {s:layoutsurl}
<br />
LayoutsUrlFull: {s:layoutsurlfull}
<br />
SiteUrl: {s:siteurl}
<br />
SiteUrlFull: {s:siteurlfull}
<br />
Scheme: {s:urlscheme}
<br />
Host: {s:urlhost}
<br />
Date: {s:date}
<br />
Time: {s:time}
<br />
DateTime: {s:datetime}
<br />
UserName: {s:username}
<br />
UserLoginFull: {s:userloginfull}
<br />
UserLogin: {s:userlogin}
<br />
UserDomain: {s:userdomain}
<br />
QS: {sq:test}
<br />
List title: {sl:title:documents}
<br />
List url: {sl:url:documents}
<br />
List schema: {sl:viewschema:documents}
<br />
List schema: {sl:viewschemaurl:documents}
<br />
List ID: {sl:id:documents}
<br />
Parameters: {sp:param1} - {sp:param2} - {sp:param3} - {sp:param4}
<br />
User Parameters: {sp:user1} - {sp:user2} - {sp:user3}
<br />


<object id="sgartMovie" classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" 
  codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0" 
  width="{sp:param1}" height="{sp:param2}" > 
  <param name="movie" value="{sq:movie}" /> 
  <param name="quality" value="high" /> 
  <param name="bgcolor" value="#ffffff" /> 
  <embed name="sgartMovie" width="{sp:param1}" height="{sp:param2}" 
    src="{sq:movie}" quality="high" bgcolor="#ffffff"
	type="application/x-shockwave-flash" 
    pluginspage="http://www.macromedia.com/go/getflashplayer"> 
  </embed> 
</object> 



<h3 id="secure">?</h3>
<script language="javascript">
var objSecure = document.getElementById("secure");
if ("{s:urlscheme}" == "https") {
  objSecure.innerText = "Secure protocol https";
} else{
  objSecure.innerText = "Normal protocol http";
}
</script>

 
