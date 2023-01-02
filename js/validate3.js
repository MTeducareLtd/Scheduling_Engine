function validate(pFromWhere)
{
	if (pFromWhere == "home3")
		frm = document.feedbackfrm3;
			
	var missing = '';
	
	if(trim(frm.name.value)=="" || trim(frm.name.value)=="Name")
		missing += '\n- your name';
	
	if(trim(frm.email.value)=="" || trim(frm.email.value)=="E-mail ID")
		missing += '\n- your email address';
	else
	{
		if(isEmail(trim(frm.email.value))==false)
		{
			missing += '\n- a valid email address';
		}	
	}
	
	if(trim(frm.phone.value)=="" || trim(frm.phone.value)=="Mobile")
		missing += '\n- your mobile number ';
	else
	{
		if(!checkPhone(frm.phone.value))
			missing += '\n- a valid mobile number ';
		
		if(frm.phone.value.length != 10)
			missing += '\n- a valid mobile number of 10 digits';
	}
	
	
	
	
	
	if (missing != '')
	{
		alert("Following information are missing :\n\n " + missing);
		return false;
	}
	else
	{
		frm.action="emailthanks-download.php"
		frm.submit();
		return true;
	}
}

/*
function validate()
  {
    frm=document.feedbackfrm; 
	
   if(trim(frm.name.value)=="")
	 {
	   alert("Enter Your Name")
	   frm.name.value="";
	   frm.name.focus();
	   return false;
     } 
	 if(trim(frm.email.value)=="")
	 {
	   alert("Enter Your Email")
	   frm.email.value="";
	   frm.email.focus();
	   return false;
	 }
	  if(trim(frm.email.value)!="")
	  {
	 if(isEmail(trim(frm.email.value))==false)
	   {
	   alert("Enter Valid Email")
	   frm.email.value="";
	   frm.email.focus();
	   return false;
	 } 
	 }
	 
	 if(trim(frm.phone.value)=="")
	 {
	   alert("Enter Your Phone No")
	   frm.phone.value="";
	   frm.phone.focus();
	   return false;
	 }
	 
	 if(trim(frm.phone.value)!="")
	 {
	   if(!checkPhone(frm.phone.value))
	   {
	   alert("Enter Valid Phone/Mob No.")
	   frm.phone.value="";
	   frm.phone.focus();
	   return false;
	   }
	   
	   
	 
	  if( document.feedbackfrm.interested.value == "-1" )
   {
     alert( "Please Select your Interested!" );
     return false;
   }
   
    if( document.feedbackfrm.city.value == "-1" )
   {
     alert( "Please provide your city!" );
     return false;
   }
	 
	 
 } 
	 
	
	
  //if(trim(frm.Comments.value)=="")
//	 {
//	   alert("Enter your Query")
//	   frm.Comments.value="";
//	   frm.Comments.focus();
//	   return false;
//	 }	

	   	 
	  frm.action="emailthanks.php"
	 frm.submit();
  }
  */
  
  function trim(inputString) 
{
   if (typeof inputString != "string") { return inputString; }
   var retValue = inputString;
   var ch = retValue.substring(0, 1);
   
   while (ch == " ") { 
      retValue = retValue.substring(1, retValue.length);
      ch = retValue.substring(0, 1);
   }
   
   ch = retValue.substring(retValue.length-1, retValue.length);
   while (ch == " ") { 
      retValue = retValue.substring(0, retValue.length-1);
      ch = retValue.substring(retValue.length-1, retValue.length);
   }
   while (retValue.indexOf("  ") != -1) { 
      retValue = retValue.substring(0, retValue.indexOf("  ")) + retValue.substring(retValue.indexOf("  ")+1, retValue.length); 
   }
   return retValue;
}


function checkname(sText)
{
    var ValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-";
    var IsNumber=true;
    var Char;
	if(sText.length==0)	
	{
	  return false;
	}
	else
	{
    for (i = 0; i < sText.length && IsNumber == true; i++) 
    { 
        Char = sText.charAt(i); 
        if (ValidChars.indexOf(Char) == -1) 
        {
            IsNumber = false;
        }
    }  
    return IsNumber;
   }	
}

function checkcity(sText)
{
    var ValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-";
    var IsNumber=true;
    var Char;
	if(sText.length==0)	
	{
	  return false;
	}
	else
	{
    for (i = 0; i < sText.length && IsNumber == true; i++) 
    { 
        Char = sText.charAt(i); 
        if (ValidChars.indexOf(Char) == -1) 
        {
            IsNumber = false;
        }
    }  
    return IsNumber;
   }	
}

function isEmail (emailIn){
	var isEmailOk = false;
	var filter = /^[a-zA-Z0-9][a-zA-Z0-9._-]*\@[a-zA-Z0-9-]+(\.[a-zA-Z][a-zA-Z-]+)+$/
	if(emailIn.search(filter) != -1)
		{
			isEmailOk = true;
		/*	var arr = emailIn.split(".");
			if(arr[1]!="edu")
		    isEmailOk = false;*/
		}
	if(emailIn.indexOf("..") != -1)
		isEmailOk = false;
	if(emailIn.indexOf(".@") != -1)
		isEmailOk = false;
	return isEmailOk;
}

	function isValid(who) {
		var invalidChars=new Array("~","!","@","#","$","%","^","&","*","(",")","+","=","[","]",":",";",",","\"","'","|","{","}","\\","/","<",">","?");
		var testArr=who.split("");
		for(var i=0; i<testArr.length; i++) {
			for(var j=0; j<invalidChars.length; j++) {
				if(testArr[i]==invalidChars[j]) {
					return false;
				}
			}
		}
		return true;
	}
	
	function checkoption(sText)
{
    var ValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-";
    var IsNumber=true;
    var Char;
	if(sText.length==0)	
	{
	  return false;
	}
	else
	{
    for (i = 0; i < sText.length && IsNumber == true; i++) 
    { 
        Char = sText.charAt(i); 
        if (ValidChars.indexOf(Char) == -1) 
        {
            IsNumber = false;
        }
    }  
    return IsNumber;
   }	
}

	
	

	function isfl(who) {
		var invalidChars=new Array("-","_",".");
		var testArr=who.split("");
		which=0;
		for(var i=0; i<2; i++) {
			for(var j=0; j<invalidChars.length; j++) {
				if(testArr[which]==invalidChars[j]) {
					return false;
				}
			}
			which=testArr.length-1;
		}
		return true;
	}

	function isDomain(who) {
		var invalidChars=new Array("-","_",".");
		var testArr=who.split("");
		if(testArr.length<2||testArr.length>4) {
			return false;
		}
		for(var i=0; i<testArr.length; i++) {
			for(var j=0; j<invalidChars.length; j++) {
				if(testArr[i]==invalidChars[j]) {
					return false;
				}
			}
		}
		return true;
	}


	

function checkPhone(strPhone)
{
	var digits = "0123456789+-()/";
	var phoneNumberDelimiters = "-";
	var phoneNumberDelimiters1 = "+";
	var validPhoneChars = phoneNumberDelimiters;
	var validPhoneChars1 = phoneNumberDelimiters1;
	s=stripCharsInBag(strPhone,validPhoneChars);
	s=stripCharsInBag(s,validPhoneChars1);
	//return (isInteger(s));
	return inValidCharSet(strPhone,digits)
}
function stripCharsInBag(s, bag)
{
    var i;
    var returnString = "";
    for (i = 0; i < s.length; i++)
    {   
        var c = s.charAt(i);
        if (bag.indexOf(c) == -1) returnString += c;
    }
    return returnString;
}

function inValidCharSet(str,charset){
	var result = true;
	for (var i=0;i<str.length;i++)
		if (charset.indexOf(str.substr(i,1))<0){
			result = false;
			break;
		}
	return result;
}

function isInteger(s)
{
    var i;
    for (i = 0; i < s.length; i++)
    {   
        // Check that current character is number.
        var c = s.charAt(i);
        if (((c < "0") || (c > "9"))) return false;
    }
     // All characters are numbers.
    return true;
}

function stripCharsInBag(s, bag)
{
    var i;
    var returnString = "";
    for (i = 0; i < s.length; i++)
    {   
        var c = s.charAt(i);
        if (bag.indexOf(c) == -1) returnString += c;
    }
    return returnString;
}

