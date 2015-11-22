// https://learn.jquery.com/plugins/basic-plugin-creation/

/*
 * $('.class').CustomPlugin({
 *		option1: 'test'
 * });
 * 
 */


(function ($) {

	// PRIVATE
	var shade = '#556b2f';

	function init() {
		alert('initialise');
	}

	// PUBLIC
	$.fn.CustomPlugin = function (options) {

		var settings = $.extend({
			option1: false
		}, options);

		init();

		this.css('color', shade);

		return this;
	};

}(jQuery));


