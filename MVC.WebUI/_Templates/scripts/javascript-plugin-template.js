/*
 * JAVASCRIPT TEMPLATE - e.g. for login page in Accounts section
 * file name: website-accounts-login.js
 */

var Website = window.Website || {};

Website.Accounts.Login = (function (options) {
	'use strict';

	// PRIVATE
	function setDefaults(options) {
		options.option1 = options.option1 || 'default';
		return options;
	}

	// PUBLIC
	return {
		init: function (options) {

			var options = options || {};
			options = setDefaults(options);

			alert(options.option1);
		}
	}

})();

