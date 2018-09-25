describe('angularjs homepage title check', function () {

    it('On Correct Page', function ()
    {
        browser.get('http://localhost:54345/');
        var title = element(by.id('Title'));
        //The Taxi Meter s the correct page.
        expect(title.getText()).toEqual('Taxi Meter');
    });

    it('Has Correct Model', function ()
    {
        browser.get('http://localhost:54345/');
        //The input fields should be visble.
        var minutesTravelled = element(by.model('formData.minutesTravelled'));
        expect(minutesTravelled.isPresent()).toEqual(true);

        var milesTravlled = element(by.model('formData.milesTravlled'));
        expect(milesTravlled.isPresent()).toEqual(true);

        var startTime = element(by.model('formData.startTime'));
        expect(startTime.isPresent()).toEqual(true);
        //The output field shouldn't be
        var fare = element(by.model('fare'));
        expect(fare.isPresent()).toEqual(false);
    });

    it('Returns Correct Value Test', function ()
    {
        browser.get('http://localhost:54345/');
        //Input values provided in the assgnment document
        var minutesTravelled = element(by.model('formData.minutesTravelled'));
        minutesTravelled.sendKeys('5');

        var milesTravlled = element(by.model('formData.milesTravlled'));
        milesTravlled.sendKeys('2');

        var startTime = element(by.model('formData.startTime'));
        startTime.sendKeys('2010-10-10T22:30:00');

        var submitButton = element(by.id('submitButton'));
        var fare = element(by.id('fare'));
        expect(fare.getText()).toEqual("");
        submitButton.click();
        browser.sleep(2000);
        expect(fare.getText()).toEqual("Total Cost: $9.25");
    });

    it('Correct Value Isnt Hardcoded Test', function ()
    {
        browser.get('http://localhost:54345/');
        //Input values provided in the assgnment document
        var minutesTravelled = element(by.model('formData.minutesTravelled'));
        minutesTravelled.sendKeys('6');

        var milesTravlled = element(by.model('formData.milesTravlled'));
        milesTravlled.sendKeys('3');

        var startTime = element(by.model('formData.startTime'));
        startTime.sendKeys('2010-11-10T12:30:00');

        var submitButton = element(by.id('submitButton'));
        var fare = element(by.id('fare'));
        expect(fare.getText()).toEqual("");
        submitButton.click();
        browser.sleep(2000);
        expect(fare.getText()).toEqual("Total Cost: $10.85");
    });
    it('Minutes Have To Be Divisible By 1/5 Test', function ()
    {
        browser.get('http://localhost:54345/');
        //Input values provided in the assgnment document
        var minutesTravelled = element(by.model('formData.minutesTravelled'));
        minutesTravelled.sendKeys('6.75');

        var milesTravlled = element(by.model('formData.milesTravlled'));
        milesTravlled.sendKeys('3');

        var startTime = element(by.model('formData.startTime'));
        startTime.sendKeys('2010-11-10T12:30:00');

        var submitButton = element(by.id('submitButton'));
        var fare = element(by.id('fare'));
        expect(fare.getText()).toEqual("");
        submitButton.click();
        browser.sleep(2000);
        expect(fare.getText()).toEqual("");
    });
    it('Miles Have to be Whole Numbers Test', function () {
        browser.get('http://localhost:54345/');
        //Input values provided in the assgnment document
        var minutesTravelled = element(by.model('formData.minutesTravelled'));
        minutesTravelled.sendKeys('2');

        var milesTravlled = element(by.model('formData.milesTravlled'));
        milesTravlled.sendKeys('3.25');

        var startTime = element(by.model('formData.startTime'));
        startTime.sendKeys('2010-11-10T12:30:00');

        var submitButton = element(by.id('submitButton'));
        var fare = element(by.id('fare'));
        expect(fare.getText()).toEqual("");
        submitButton.click();
        browser.sleep(2000);
        expect(fare.getText()).toEqual("");
    });
});
