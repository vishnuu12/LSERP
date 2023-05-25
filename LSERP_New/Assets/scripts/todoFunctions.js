/* INPUTS */		
$(document).ready(function () {
	$('.checkbox').iCheck({
		checkboxClass: 'icheckbox_square-pink',
		increaseArea: '20%'
	});	
});	


/*TO DO FUNCTION */
$(".todo ul").sortable();

$("#add_todo").on('submit', function (e) {
	e.preventDefault();

	var $toDo = $(this).find('.name-of-todo'),
		toDo_name = $toDo.val();
	if (toDo_name.length >= 3) {
		var newid = "new" + "" + Math.random().toString(36).substring(11);
		// Create Event Entry
		$(".todo ul").append(
			'<li id="' + 'item-' + newid + '" class="list-group-item"><div class="checkbox"><input class="update-default-option" type="checkbox" id="' + newid + '" /><label for="' + newid + '">' + toDo_name + '</label></div><div class="pull-right action-btns"><a href="#" class="archive"><span class="fa fa-archive"></span></a><a href="#" class="trash"><span class="fa fa-close"></span></a></div></li>'
		);
		
		var eventObject = {
			title: $.trim($("#" + newid).text()),
			className: $("#" + newid).attr("data-bg"),
			stick: true
		};
		$("#" + newid).data('eventObject', eventObject);
		// Reset input
		$toDo.val('').focus();
		
		$('.todo .list-group-item .checkbox').iCheck('destroy');
		$('.todo .list-group-item .checkbox').iCheck({
			checkboxClass: 'icheckbox_square-pink',
			increaseArea: '20%'
		});
		$('.todo .list-group-item .checkbox').iCheck('update');
	} else {
		$toDo.focus();
	}
});

//Checking items
$(document).on('ifChecked', '.todo .list-group-item .checkbox', function(event){
  $(this).closest(".list-group-item").not(".archive-item").addClass("checked-todo");
});

$(document).on('ifUnchecked', '.todo .list-group-item .checkbox', function(event){
  $(this).closest(".list-group-item").removeClass("checked-todo");
});

//Check archive items
$(document).on('click', '.action-btns .archive', function (e) {
	$(this).closest(".list-group-item").addClass("archive-item").hide();
	e.preventDefault();
});

//Show all items
$(document).on('click', '.all-todo', function (e) {
	$('.todo .list-group-item').hide().not(".archive-item").show();
	e.preventDefault();
});

//Show only active items
$(document).on('click', '.active-todo', function (e) {
	$('.todo .list-group-item').hide().not(".archive-item").not(".checked-todo").show();
	e.preventDefault();
});

//Show only completed items
$(document).on('click', '.complete-todo', function (e) {
	$('.checked-todo').show();
	$('.todo .list-group-item').not(".checked-todo").hide();
	e.preventDefault();
});

//Show only archive items
$(document).on('click', '.archive-todo', function (e) {
	$('.archive-item').show();
	$('.todo .list-group-item').not(".archive-item").hide();
	e.preventDefault();
});

	

//Remove one completed item
$(document).on('click', '.trash', function (e) {
	var clearedCompItem = $(this).closest(".list-group-item").remove();
	e.preventDefault();
});

//Mark all as complete
$(document).on('ifChecked', 'input#clear', function(event){
  $('.list-group .checkbox input').not(".archive-item .checkbox input").iCheck('check');
});

$(document).on('ifUnchecked', 'input#clear', function(event){
  $('.list-group .checkbox input').iCheck('uncheck');
});


//Remove all completed items
$(document).on('click', '#clear-comp', function (e) {
	var clearedComp = $('.checked').closest(".list-group-item").remove();
	$('input#clear').iCheck('uncheck');
	e.preventDefault();
});

