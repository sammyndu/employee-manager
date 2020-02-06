// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('#custom-headers').multiSelect({
    selectableHeader: "<div class='custom-header text-center'>Users</div>",
    selectionHeader: "<div class='custom-header text-center'>Direct Subordinates</div>"
});

$('#exampleModalCenter').on('show.bs.modal', function(e){    
    $('.modal-body').html('');
    $('.modal-body').append(`Are you sure you want to delete this user <strong>${$(e.relatedTarget).data("email")}</strong>?`);
    $('#delete').attr('action', `Admin/Delete/${$(e.relatedTarget).data("id")}`);
});

$("#User_YearlySalary").on('focus', function(){
    let salary = $('#User_YearlySalary').val();
    let newsalary = parseInt(salary).toLocaleString(undefined, {minimumFractionDigits:0});
    console.log(newsalary);
    $('#User_YearlySalary').attr('type', 'text');
    $('#User_YearlySalary').val(newsalary);
})

$("#User_YearlySalary").on('mouseout', function(){
    $(this).blur();
    salary = $(this).val();
    salary = salary.replace(/\D/g, '');
    $('#User_YearlySalary').val(salary);
    $('#User_YearlySalary').attr('type', 'number');
    
})

$("#User_YearlySalary").on('input', function(){
    salary = $(this).val();
    salary = salary.replace(/\D/g, '');
    salary = parseInt(salary).toLocaleString(undefined, {minimumFractionDigits:0});
    $('#User_YearlySalary').val(salary);    
})