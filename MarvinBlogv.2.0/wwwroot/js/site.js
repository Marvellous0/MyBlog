// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const addLikes = (id) => {
    fetch(`/review/AddLike/${id}`)
        .then(res => res.json())
        .then(res => {
            console.log(res);
            const post_likes = document.querySelector(`#post_likes_${id}`);
            post_likes.textContent = res.likes;
        })
}

const addFollowers = (id) => {
    fetch(`/blogger/AddFollower/${id}`)
        .then(res => res.json())
        .then(res => {
            console.log(res);
        })
}

const addFollowersFromProfile = (id) => {
    fetch(`/blogger/AddFollowerFromProfile/${id}`)
        .then(res => res.json())
        .then(res => {
            console.log(res);
        })
}

const addFollowersFromListUsers = (id) => {
    fetch(`/blogger/AddFollowerFromListUsers/${id}`)
        .then(res => res.json())
        .then(res => {
            console.log(res);
        })
}
