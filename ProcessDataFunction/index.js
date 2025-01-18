module.exports = async function (context, req) {
    context.log('Processing HTTP request in JavaScript.');

    try {
        const { name, age } = req.body;

        if (name && age) {
            context.res = {
                status: 200,
                body: {
                    message: `Hello ${name}, you are ${age} years old!`
                }
            };
        } else {
            context.res = {
                status: 400,
                body: "Invalid input. Please provide 'name' and 'age'."
            };
        }
    } catch (err) {
        context.res = {
            status: 400,
            body: "Invalid JSON format."
        };
    }
};
