// wwwroot/js/footer.js
document.addEventListener('DOMContentLoaded', function() {
    // Newsletter Form
    var newsletterForm = document.querySelector('form[name="new-letter-foter"]');
    if (newsletterForm) {
        newsletterForm.addEventListener('submit', function(e) {
            e.preventDefault();
            var emailInput = this.querySelector('.newsletter-email');
            if (emailInput.value && emailInput.value.includes('@')) {
                alert('Thank you for subscribing to QUICKMART newsletter!');
                this.reset();
            } else {
                alert('Please enter a valid email address.');
            }
        });
    }
    
    // Mobile Footer Visibility
    function checkMobileView() {
        var mobileFooter = document.querySelector('.mobile-footer');
        if (window.innerWidth <= 768) {
            if (mobileFooter) mobileFooter.style.display = 'block';
        } else {
            if (mobileFooter) mobileFooter.style.display = 'none';
        }
    }
    
    checkMobileView();
    window.addEventListener('resize', checkMobileView);
});