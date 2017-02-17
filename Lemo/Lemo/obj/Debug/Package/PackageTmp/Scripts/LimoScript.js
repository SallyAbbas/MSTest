function Run(func) {
    var prm;
    try {
        prm = Sys.WebForms.PageRequestManager.getInstance();
    }
    catch (ex) { }

    if (prm != undefined) {
        prm.add_pageLoaded(func);
    }
    else {
        $(func);
    }
}