var title = document.getElementById("Title");
var slug = document.getElementById("UrlSlug");

title.addEventListener("keyup", function (e) {
    slug.value = title.value
        .trim()
        .replace(/[\W_]+/g, "-")
        .toLowerCase();
    return true;
});