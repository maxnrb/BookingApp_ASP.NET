// Script for simple search in CRUDs
$(document).ready(function () {
	$("#searchInput").on("keyup", function () {
		var value = $(this).val().toLowerCase();
		$("#resultTable tr").filter(function () {
			$(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
		});
	});
});	


// Auto upload picture(s) when selected (for ManagePictures.cshtml)
$(document).ready(function () {
	$("#uploadPictures").on("change", function () {
		$("#uploadPicturesForm").submit();
	});

	$("#uploadPicturesBtn").hide();
});