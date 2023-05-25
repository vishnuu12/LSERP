// JScript File

/* Common Validation */

//Numeric Validation - only Allow for Numbers [0-9]
function validationNumeric(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    else {
        return true;
    }
}

//Currency Validation - only Allow for Numbers [0-9] and Dot [.]
function validationCurrency(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        if (charCode == 46) {
            return true;
        }
        return false;
    }
    else {
        return true;
    }
}

//Decimal Validation - only Allow for Numbers [0-9] and Dot [.]
function validationDecimal(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        if (charCode == 46) {
            return true;
        }
        return false;
    }
    else {
        return true;
    }
}

//Date Validation - only Allow for Numbers [0-9] and Dot [/]
function validationDate(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        if (charCode == 47) {
            return true;
        }
        return false;
    }
    else {
        return true;
    }
}

// Only Alphabets [ Alpahabets, Space , BackSpace ]
function validationAlphabets(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || (charCode == 32) || (charCode == 8)) {
        return true;
    }
    else {
        return false;
    }
}

// Only Alphabets [ Alpahabets, Numbers, Space , BackSpace ]
function validationAlphaNumeric(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || (charCode == 32) || (charCode == 8) || (charCode >= 48 && charCode <= 57)) {
        return true;
    }
    else {
        return false;
    }
}

// Code Validation [ Alpahabets, Numbers , - , / , Space , BackSpace ]
function validateCode(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || (charCode >= 48 && charCode <= 57) || (charCode == 45) || (charCode == 47) || (charCode == 32) || (charCode == 8)) {
        return true;
    }
    else {
        return false;
    }
}

// Name Validation [ Alpahabets,&, dot , Space , BackSpace ]
function validateNameWithSC(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || (charCode == 38) || (charCode == 46) || (charCode == 32) || (charCode == 8)) {
        return true;
    }
    else {
        return false;
    }
}

// Name Validation [ Alpahabets, dot , Space , BackSpace ]
function validateName(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || (charCode == 46) || (charCode == 32) || (charCode == 8)) {
        return true;
    }
    else {
        return false;
    }
}

// Address Validation [ Alpahabets, Numbers ,# , . , comma , - , ( , ) , / , Space , BackSpace , EnterKey ]
function validateAddress(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || (charCode >= 48 && charCode <= 57) || (charCode == 35) || (charCode == 46) || (charCode == 44) || (charCode == 45) || (charCode == 47) || (charCode == 40) || (charCode == 41) || (charCode == 32) || (charCode == 8) || (charCode == 13)) {
        return true;
    }
    else {
        return false;
    }
}

// City Validation [ Alpahabets, Space , BackSpace ]
function validateCity(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || (charCode == 32) || (charCode == 8)) {
        return true;
    }
    else {
        return false;
    }
}

// Phone no Validation [ Numbers ,comma, - , + , Space , BackSpace ]
function validatePhoneNo(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if ((charCode >= 48 && charCode <= 57) || (charCode == 44) || (charCode == 45) || (charCode == 43) || (charCode == 32) || (charCode == 8)) {
        return true;
    }
    else {
        return false;
    }
}

// FAX No Validation [ Numbers ,comma, - , + , Space , BackSpace ]
function validateFAXNo(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if ((charCode >= 48 && charCode <= 57) || (charCode == 44) || (charCode == 45) || (charCode == 43) || (charCode == 32) || (charCode == 8)) {
        return true;
    }
    else {
        return false;
    }
}

// Email Validation [ Alphabets, Numbers , @ , dot , BackSpace ]
function validateEmail(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || (charCode >= 48 && charCode <= 57) || (charCode == 64) || (charCode == 46) || (charCode == 8)) {
        return true;
    }
    else {
        return false;
    }
}

// Website Validation [ Alphabets, Numbers , :, /, @ , dot , BackSpace  ]
function validateWebsite(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || (charCode >= 48 && charCode <= 57) || (charCode == 58) || (charCode == 47) || (charCode == 64) || (charCode == 46) || (charCode == 8)) {
        return true;
    }
    else {
        return false;
    }
}

// Date Validation [ Numbers , / , BackSpace ]
function validateDate(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if ((charCode >= 48 && charCode <= 57) || (charCode == 44) || (charCode == 47) || (charCode == 8)) {
        return true;
    }
    else {
        return false;
    }
}

//// To check Website Address
function isValidURL(url) {
    //var RegExp = /^(([\w]+:)?\/\/)?(([\d\w]|%[a-fA-f\d]{2,2})+(:([\d\w]|%[a-fA-f\d]{2,2})+)?@)?([\d\w][-\d\w]{0,253}[\d\w]\.)+[\w]{2,4}(:[\d]+)?(\/([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)*(\?(&?([-+_~.\d\w]|%[a-fA-f\d]{2,2})=?)*)?(#([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)?$/;
    var RegExp = /^(http|https|ftp):\/\/(([\w]+:)?\/\/)?(([\d\w]|%[a-fA-f\d]{2,2})+(:([\d\w]|%[a-fA-f\d]{2,2})+)?@)?([\d\w][-\d\w]{0,253}[\d\w]\.)+[\w]{2,4}(:[\d]+)?(\/([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)*(\?(&?([-+_~.\d\w]|%[a-fA-f\d]{2,2})=?)*)?(#([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)?$/;
    if (RegExp.test(url)) {
        return true;
    } else {
        return false;
    }
}

//// To check Email Address
function isValidEmail(email) {
    var RegExp = /^((([a-z]|[0-9]|!|#|$|%|&|'|\*|\+|\-|\/|=|\?|\^|_|`|\{|\||\}|~)+(\.([a-z]|[0-9]|!|#|$|%|&|'|\*|\+|\-|\/|=|\?|\^|_|`|\{|\||\}|~)+)*)@((((([a-z]|[0-9])([a-z]|[0-9]|\-){0,61}([a-z]|[0-9])\.))*([a-z]|[0-9])([a-z]|[0-9]|\-){0,61}([a-z]|[0-9])\.)[\w]{2,4}|(((([0-9]){1,3}\.){3}([0-9]){1,3}))|(\[((([0-9]){1,3}\.){3}([0-9]){1,3})\])))$/
    if (RegExp.test(email)) {
        return true;
    } else {
        return false;
    }
} 


// Validates that the input string is a valid date formatted as "dd/mm/yyyy"
function isValidDate(dateString) {

    // First check for the pattern
    if (!/^\d{2}\/\d{2}\/\d{4}$/.test(dateString))
        return false;

    // Parse the date parts to integers
    var parts = dateString.split("/");
    var day = parseInt(parts[0], 10);
    var month = parseInt(parts[1], 10);
    var year = parseInt(parts[2], 10);

    // Check the ranges of month and year
    if (year < 1000 || year > 3000 || month == 0 || month > 12)
        return false;

    var monthLength = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

    // Adjust for leap years
    if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
        monthLength[1] = 29;

    // Check the range of the day
    return day > 0 && day <= monthLength[month - 1];
}

function checkDate(sender, args) {
    alert("hi");
    var toDate = new Date();
    toDate.setMinutes(0);
    toDate.setSeconds(0);
    toDate.setHours(0);
    toDate.setMilliseconds(0);
    if (sender._selectedDate < toDate) {
        alert("You cannot select a day earlier than today!");
        sender._selectedDate = toDate;
        //set the date back to the current date
        sender._textbox.set_Value(sender._selectedDate.format(sender._format))
    }

    //            if (sender._selectedDate < new Date()) {
    //                alert("You cannot select a day earlier than today!");
    //                sender._selectedDate = new Date();
    //                // set the date back to the current date
    //                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
    //            }
}

function checkTextAreaMaxLength(textBox, e, length) {
    var mLen = textBox["MaxLength"];
    if (null == mLen)
        mLen = length;

    var maxLength = parseInt(mLen);
    //if (!checkSpecialKeys(e)) {
        if (textBox.value.length > maxLength - 1) {
            if (window.event)//IE
            {
                e.returnValue = false;
                return false;
            }
            else//Firefox
                e.preventDefault();
        }
    //}
}

function checkSpecialKeys(e) {
    if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 35 && e.keyCode != 36 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
        return false;
    else
        return true;
}     

function ShowLoader() {
    $find("ContentPlaceHolder1_mpeLoader").show();
}
function HideLoader() {
    $find("ContentPlaceHolder1_mpeLoader").show();
}

function ShowLoaderWOMPage() {
    $find("mpeLoader").show();
}
function HideLoaderWOMPage() {
    $find("mpeLoader").show();
}




