/**
 * Minified by jsDelivr using Terser v3.14.1.
 * Original file: /gh/XigeTime/FontAwesome-Selector@master/cdn/vUNSTABLE1/faSelectorWidget.js
 *
 * Do NOT use SRI with dynamically generated files! More information: https://www.jsdelivr.com/using-sri-with-dynamic-files
 */
var selectedFaIconList, faIcons, FASIconVersion = "5.7.2", FASiconUrl = "https://cdn.jsdelivr.net/gh/XigeTime/FontAwesome-Selector/cdn/faicons-v" + FASIconVersion + ".json", faElCount = !1;
function selectFaIcon(e, t) { }
function openFaSelector(e, t) {
    try {
        if (t.target.classList.contains("active") == true || t.target.parentElement.className == 'fa-child-container') {
            $('#iconview,#ContentPlaceHolder1_iconview').removeAttr('class');
        }
        else {
            $('#iconview,#ContentPlaceHolder1_iconview').attr("class", "iconactive");
        }
    }
    catch (er) { }
    if (t.target.parentElement.classList.contains("fa-child-container")) {
        if ("selectFaIcon(this,event)" != t.target.getAttribute("onclick"))
            return !1
    } else
        t.target.children.length ? t.target.classList.contains("active") ? (t.target.classList.toggle("active"),
            t.target.children[0].style.display = "none") : (t.target.classList.toggle("active"),
                t.target.offsetHeight != t.target.children[0].style.bottom && (t.target.children[0].style.bottom = t.target.offsetHeight + 10),
                t.target.children[0].style.display = "",
                t.target.children[0].children[0].focus()) : loadJSON(function (e) {
                    faIcons = JSON.parse(e),
                        selectedFaIconList = JSON.parse(e).solid,
                        t.target.classList.toggle("active"),
                        createFaElements(t),
                        bindSearchTimers()
                })
}
function toggleFaIconStyle(e, t, a) {
    selectedFaIconList = e,
        document.querySelectorAll('.fa-child-container[data-num="' + t + '"] > i').forEach(function (e) {
            e.parentNode.removeChild(e)
        }),
        faSearchInput = document.querySelector('.fa-child-container[data-num="' + t + '"] > input');
    var n = !1;
    if ("" != faSearchInput.value) {
        n = !0;
        faIconSearchResults = [],
            selectedFaIconList.forEach(function (e) {
                e.includes(faSearchInput.value) && faIconSearchResults.push(e)
            });
        e = faIconSearchResults
    }
    populateFaIcons(n, faElCount, e)
}
function createFaElements(e) {
    createContainer(e),
        createFaIconSearch(),
        populateFaIcons(!1, faElCount, selectedFaIconList)
}
function createContainer(e) {
    var t = document.createElement("div")
        , a = document.createElement("div");
    document.createElement("style");
    faElCount ? faElCount++ : faElCount = 1,
        t.setAttribute("class", "fa-selector-container"),
        a.setAttribute("class", "fa-child-container"),
        a.setAttribute("style", "bottom:" + (e.target.offsetHeight + 10) + "px;"),
        a.setAttribute("data-num", faElCount),
        e.target.setAttribute("data-num", faElCount),
        faContainerNode = t.appendChild(a)
}
function populateFaIcons(e, t, a) {
    iconsToPopulate = 1 == e ? faIconSearchResults : selectedFaIconList,
        iconsToPopulate.forEach(function (e) {
            if (selectedFaIconList == faIcons.regular)
                var t = "far";
            else if (selectedFaIconList == faIcons.brands)
                t = "fab";
            else
                t = "fas";
            var a = document.createElement("i")
                , n = t + " fa-" + e;
            a.setAttribute("class", n),
                a.setAttribute("onclick", "selectFaIcon(this,event)"),
                faContainerNode.appendChild(a)
        }),
        t ? document.querySelector("#fa-selector[data-num='" + t + "']").appendChild(faContainerNode) : document.querySelector("#fa-selector.active").appendChild(faContainerNode)
}
function createFaIconSearch() {
    var e = document.createElement("input");
    e.setAttribute("class", "search-fa-selector"),
        e.setAttribute("placeholder", "Search icons..."),
        e.setAttribute("autofocus", "autofocus"),
        faContainerNode.appendChild(e)
}
function bindSearchTimers() {
    var e;
    faSearchInterval = 500,
        document.querySelectorAll(".fa-child-container input").forEach(function (t) {
            t.addEventListener("keyup", function (t) {
                clearTimeout(e),
                    e = setTimeout(runFaIconSearch(t), faSearchInterval)
            }),
                t.addEventListener("keydown", clearTimeout(e))
        })
}
function runFaIconSearch(e) {
    faIconSearchResults = [],
        faSearchInput = e.target.value;
    var t = e.target.parentElement.getAttribute("data-num");
    document.querySelectorAll('.fa-child-container[data-num="' + t + '"] > i').forEach(function (e) {
        e.parentNode.removeChild(e)
    }),
        selectedFaIconList.forEach(function (e) {
            e.includes(faSearchInput) && faIconSearchResults.push(e)
        }),
        populateFaIcons(!0, t, selectedFaIconList),
        e.target.focus()
}
function loadJSON(e) {
    var t = new XMLHttpRequest;
    t.overrideMimeType("application/json"),
        t.open("GET", FASiconUrl, !0),
        t.onreadystatechange = function () {
            4 == t.readyState && "200" == t.status && e(t.responseText)
        }
        ,
        t.send(null)
}
//# sourceMappingURL=/sm/5b45cb2be9c94e7c5796497436377c9eda57d9f47d1282b99c81b818b4b35f0b.map
