function toggleVisibility(element) {
    var style = element.style.getPropertyValue("visibility");
    if (style === "visible") {
        style = "collapse";
    }
    else {
        style = "visible";
    }
    element.style.setProperty("visibility", style);
}
document.addEventListener("DOMContentLoaded", function () {
    var thing = document.getElementById("thing");
    var p = document.getElementById("p");
    if (thing != null && p != null) {
        var thang_1 = thing;
        p.addEventListener("click", function () { return toggleVisibility(thang_1); });
    }
});
