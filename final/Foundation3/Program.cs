using System;

// =========================== ADDRESS CLASS ===========================
class Address
{
    private string street;
    private string city;
    private string state;
    private string country;

    public Address(string street, string city, string state, string country)
    {
        this.street = street;
        this.city = city;
        this.state = state;
        this.country = country;
    }

    public string GetFullAddress()
    {
        return $"{street}, {city}, {state}, {country}";
    }
}

// =========================== BASE EVENT CLASS ===========================
class Event
{
    private string title;
    private string description;
    private string date;
    private string time;
    private Address address;

    public Event(string title, string description, string date, string time, Address address)
    {
        this.title = title;
        this.description = description;
        this.date = date;
        this.time = time;
        this.address = address;
    }

    public string GetStandardDetails()
    {
        return $"Event: {title}\nDescription: {description}\nDate: {date}" +
               $"\nTime: {time}\nAddress: {address.GetFullAddress()}";
    }

    // Overridden in derived classes
    public virtual string GetFullDetails()
    {
        return GetStandardDetails();
    }

    public virtual string GetShortDescription()
    {
        return $"Event Type: General\nTitle: {title}\nDate: {date}";
    }
}

// =========================== LECTURE CLASS ===========================
class Lecture : Event
{
    private string speakerName;
    private int capacity;

    public Lecture(string title, string description, string date, string time, Address address,
                   string speakerName, int capacity)
        : base(title, description, date, time, address)
    {
        this.speakerName = speakerName;
        this.capacity = capacity;
    }

    public override string GetFullDetails()
    {
        return $"{base.GetStandardDetails()}" +
               $"\nType: Lecture\nSpeaker: {speakerName}\nCapacity: {capacity}";
    }

    public override string GetShortDescription()
    {
        return $"Event Type: Lecture\nTitle: {speakerName} - Lecture\nDate: {DateTime.Parse("01/01/2000")}";
    }
}

// =========================== RECEPTION CLASS ===========================
class Reception : Event
{
    private string rsvpEmail;

    public Reception(string title, string description, string date, string time, Address address,
                     string rsvpEmail)
        : base(title, description, date, time, address)
    {
        this.rsvpEmail = rsvpEmail;
    }

    public override string GetFullDetails()
    {
        return $"{base.GetStandardDetails()}" +
               $"\nType: Reception\nRSVP Email: {rsvpEmail}";
    }

    public override string GetShortDescription()
    {
        return $"Event Type: Reception\nTitle: (Reception) {DateTime.Parse("01/01/2000")}\nDate: ???";
    }
}

// =========================== OUTDOOR GATHERING CLASS ===========================
class OutdoorGathering : Event
{
    private string weather;

    public OutdoorGathering(string title, string description, string date, string time, Address address,
                            string weather)
        : base(title, description, date, time, address)
    {
        this.weather = weather;
    }

    public override string GetFullDetails()
    {
        return $"{base.GetStandardDetails()}" +
               $"\nType: Outdoor Gathering\nWeather Forecast: {weather}";
    }

    public override string GetShortDescription()
    {
        return $"Event Type: Outdoor Gathering\nTitle: Outdoor Event\nDate: ???";
    }
}

// =========================== MAIN PROGRAM ===========================
class Program
{
    static void Main(string[] args)
    {
        // LECTURE EVENT
        Address addr1 = new Address("100 Main St", "New York", "NY", "USA");
        Lecture lecture = new Lecture("Tech Innovations", "A talk on future tech", "04/15/2025", "6:00 PM",
                                      addr1, "Dr. Jane Smith", 150);

        // RECEPTION EVENT
        Address addr2 = new Address("250 Ocean Ave", "Miami", "FL", "USA");
        Reception reception = new Reception("Networking Night", "A casual business social event",
                                            "05/10/2025", "7:30 PM", addr2, "rsvp@events.com");

        // OUTDOOR GATHERING EVENT
        Address addr3 = new Address("Green Park", "Denver", "CO", "USA");
        OutdoorGathering gathering = new OutdoorGathering("Summer Festival",
                                                          "Food, games, and music outdoors",
                                                          "07/04/2025", "12:00 PM",
                                                          addr3, "Sunny, 78°F");

        // DISPLAY RESULTS
        Event[] events = { lecture, reception, gathering };

        foreach (Event ev in events)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine(ev.GetStandardDetails());
            Console.WriteLine();
            Console.WriteLine(ev.GetFullDetails());
            Console.WriteLine();
            Console.WriteLine(ev.GetShortDescription());
            Console.WriteLine("--------------------------------------------------\n");
        }
    }
}
