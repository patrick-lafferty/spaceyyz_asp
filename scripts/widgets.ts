function toggleVisibility(element: HTMLElement) {
	let style = element.style.getPropertyValue("visibility");

	if (style === "visible") {
		style = "collapse";
	} 
	else {
		style = "visible";
	}

	element.style.setProperty("visibility", style);
}

document.addEventListener("DOMContentLoaded", function () {
	let thing = document.getElementById("thing");
	let p = document.getElementById("p");

	if (thing != null && p != null) {
		let thang: HTMLElement = thing;
		p.addEventListener("click", () => toggleVisibility(thang));
	}

});
