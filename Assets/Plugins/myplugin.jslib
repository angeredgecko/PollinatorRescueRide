mergeInto(LibraryManager.library, {

  SyncData: function () {
    FS.syncfs(false, function (err) {
    });
  },

});