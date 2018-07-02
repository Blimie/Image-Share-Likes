$(() => {   
    $("#like").on('click', function () {      
        $.post('/Home/IncrementLikeCount', { imageId: $(this).data('image-id') }, function () {  
            $("#like").prop('disabled', true);
        });
    });   
   
    setInterval(function () {      
        $.get('/Home/GetLikeCount', { imageId: $("#like").data('image-id') }, function (results) {
            $("#like-count").text(results.Likes);
        });
    }, 1000);    
});